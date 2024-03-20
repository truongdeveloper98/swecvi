using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.ApplicationCore.Interfaces.Services;
using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.Infrastructure.Services
{
    public class PythonCodeService : IPythonCodeService
    {
        private readonly IPythonCodeRepository _pythonCodeRepository;
        public PythonCodeService(IPythonCodeRepository pythonCodeRepository)
        {
            _pythonCodeRepository = pythonCodeRepository;
        }

        public async Task<List<PythonCodeVersionDto.CodeVersion>> GetPythonCodes()
        {
            List<PythonCodeVersionDto.CodeVersion> pythonCodeVersions = new List<PythonCodeVersionDto.CodeVersion>();

            var groupVersion = (await _pythonCodeRepository.QueryAsync(includeProperties: "PythonDefault")).GroupBy(m => m.PythonDefault.FileName);

            foreach (var item in groupVersion)
            {
                PythonCodeVersionDto.CodeVersion codeVersions = new PythonCodeVersionDto.CodeVersion();
                codeVersions.FileName = item.Key;
                codeVersions.Versions = item.Select(m => new PythonCodeVersionDto.PythonCode
                {
                    Id = m.Id,
                    Version = m.Version,
                    Script = m.Script,
                    IsCurrentVersion = m.IsCurrentVersion,

                }).OrderBy(m => m.Id).ToList();
                pythonCodeVersions.Add(codeVersions);
            }
            return pythonCodeVersions;
        }
        public async Task CreatePythonCode(int id, string script)
        {
            var existedPythonCode = (await _pythonCodeRepository.QueryAsync(m => m.Id == id, includeProperties: "PythonDefault")).FirstOrDefault();
            if (existedPythonCode == null)
            {
                throw new Exception("Python code not found");
            }
            var pythonCodes = await _pythonCodeRepository.QueryAsync(m => m.IsCurrentVersion && m.PythonDefault.Id == existedPythonCode.PythonDefault.Id);

            foreach (var it in pythonCodes)
            {
                it.IsCurrentVersion = false;
                await _pythonCodeRepository.Update(it);
            }
            var latestVersion = (await _pythonCodeRepository.QueryAsync(m => m.PythonDefault.Id == existedPythonCode.PythonDefault.Id, includeProperties: "PythonDefault")).Max(p => p.Version);

            var pythonVersion = new PythonCode
            {
                Script = script,
                IsCurrentVersion = true,
                Path = existedPythonCode.Path,
                PythonDefaultId = existedPythonCode.PythonDefault.Id,
                Version = latestVersion + 1
            };
            await _pythonCodeRepository.Add(pythonVersion);
            UpdatePythonFile(script, existedPythonCode.Path!);
        }

        public async Task DeletePythonCode(int id)
        {
            var existedPythonCode = await _pythonCodeRepository.Get(id, includeProperties: "PythonDefault");
            if (existedPythonCode == null)
            {
                throw new Exception("Python code not found");
            }
            if (existedPythonCode.IsDefault)
            {
                throw new Exception("Cannot remove default python code");
            }
            if (existedPythonCode.IsCurrentVersion)
            {
                var defaultPythonCode = await _pythonCodeRepository.Get(e => e.IsDefault && e.PythonDefault.Id == existedPythonCode.PythonDefault.Id);
                if (defaultPythonCode == null)
                {
                    throw new Exception("Default python code not found");
                }
                defaultPythonCode.IsCurrentVersion = true;
                await _pythonCodeRepository.Update(defaultPythonCode);
                UpdatePythonFile(defaultPythonCode.Script!, existedPythonCode.Path!);
            }
            await _pythonCodeRepository.Delete(existedPythonCode);
        }

        public async Task SetCurrentVersion(int id)
        {
            var targetPythonCode = await _pythonCodeRepository.Get(id, includeProperties: "PythonDefault");
            if (targetPythonCode == null)
            {
                throw new Exception("Python code not found");
            }

            if (!targetPythonCode.IsCurrentVersion)
            {
                var pythonCodes = await _pythonCodeRepository.QueryAsync(m => m.IsCurrentVersion && m.PythonDefault.Id == targetPythonCode.PythonDefault.Id);
                foreach (var it in pythonCodes)
                {
                    it.IsCurrentVersion = false;
                    await _pythonCodeRepository.Update(it);
                }
                if (targetPythonCode != null)
                {
                    targetPythonCode.IsCurrentVersion = true;
                    await _pythonCodeRepository.Update(targetPythonCode);
                    UpdatePythonFile(targetPythonCode.Script!, targetPythonCode.Path!);
                }
            }
        }


        public async Task ResetDefault(int id, bool force = false)
        {
            var existedPythonCode = await _pythonCodeRepository.Get(id, includeProperties: "PythonDefault");
            if (existedPythonCode == null)
            {
                throw new Exception("Python code not found");
            }
            var defaultPythonCode = (await _pythonCodeRepository.QueryAsync(e => e.IsDefault && e.PythonDefault == existedPythonCode.PythonDefault)).FirstOrDefault();
            var pythonCodes = await _pythonCodeRepository.QueryAsync(e => e.IsCurrentVersion && e.PythonDefault == existedPythonCode.PythonDefault && e.Id != id);

            if (existedPythonCode.PythonDefault == null || defaultPythonCode == null)
            {
                throw new Exception("Default python code not found");
            }
            foreach (var it in pythonCodes)
            {
                it.IsCurrentVersion = false;
                await _pythonCodeRepository.Update(it);
            }
            if (force)
            {
                string defaultContent = File.ReadAllText(defaultPythonCode.Path!);
                defaultPythonCode.Script = defaultContent;
            }
            defaultPythonCode.IsCurrentVersion = true;
            UpdatePythonFile(defaultPythonCode.Script!, existedPythonCode.Path!);
            await _pythonCodeRepository.Update(existedPythonCode);
        }
        private void UpdatePythonFile(string content, string path)
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write(content);
            }
        }
    }
}