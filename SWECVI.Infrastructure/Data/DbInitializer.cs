using SWECVI.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.ApplicationCore.Interfaces.Repositories;
using SWECVI.ApplicationCore.ViewModels.Hospital;
using SWECVI.ApplicationCore.Common;
using SWECVI.ApplicationCore.Constants;

namespace SWECVI.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IUserRepository userRepo, IRegionRepository regionRepository, IHospitalRepository hospitalRepository)
        {
            var region = new Region()
            {
                Name = "Stockholm"
            };

            await regionRepository.Add(region);


            var hospital = new Hospital()
            {
                Name = "SuperAdmin",
                ConnectionString = "Server=13.238.18.123;Database=coreDbSwecviAdmin;Persist Security Info=True;User ID=sa;Password=Asdfgh1@3;TrustServerCertificate=true",
                IndexRegion = region.Id
            };

            await hospitalRepository.Add(hospital);


            if (!(await roleManager.RoleExistsAsync("SuperAdmin")))
            {
                AppRole role = new AppRole
                {
                    Name = "SuperAdmin"
                };
                await roleManager.CreateAsync(role);
            }
            if (!(await roleManager.RoleExistsAsync("User")))
            {
                AppRole role = new AppRole
                {
                    Name = "User"
                };
                await roleManager.CreateAsync(role);
            }
            if (userManager.FindByEmailAsync("developer.brightsoft@gmail.com").Result == null)
            {
                var appUser = new AppUser
                {
                    UserName = "BRSS",
                    Email = "developer.brightsoft@gmail.com",
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(appUser, "Asdfgh1@3").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(appUser, "SuperAdmin").Wait();
                }

                var user = new User
                {
                    FirstName = "BRSS",
                    LastName = "BRSS",
                    FullName = "BRSS Developer",
                    Identity = appUser,
                };
                await userRepo.Add(user);
            }
        }

        public static async Task SeedUserByHospital(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IUserRepository userRepo, string email, string password, int? indexDepartment,HopsitalViewModel model, IHospitalRepository hospitalRepository, IRegionRepository regionRepository) 
        {
            var region = new Region()
            {
                Name = "Stockholm"
            };

            await regionRepository.Add(region);

            if (!(await roleManager.RoleExistsAsync("HospitalAdmin")))
            {
                AppRole role = new AppRole
                {
                    Name = "HospitalAdmin"
                };
                await roleManager.CreateAsync(role);
            }
            if (!(await roleManager.RoleExistsAsync("User")))
            {
                AppRole role = new AppRole
                {
                    Name = "User"
                };
                await roleManager.CreateAsync(role);
            }
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                var appUser = new AppUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(appUser, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(appUser, "HospitalAdmin").Wait();
                }

                var user = new User
                {
                    FirstName = "Hospital Admin",
                    LastName = "Hospital Admin",
                    FullName = "Hospital Admin",
                    Identity = appUser,
                };

                await userRepo.Add(user);

                var hospital = new Hospital()
                {
                    Name = model.HospitalName,
                    IndexRegion = region.Id,
                    ConnectionString = model.ConnectionString,
                    IsDeleted = false
                };

                await hospitalRepository.Add(hospital);
            }
        }

        //public static void SeedParameterSetting(IParameterSettingRepository parameterSettingRepository, ISystemLogRepository systemLogRepository)
        //{
        //    //try
        //    //{
        //    //    var isExisted = (await parameterSettingRepository.Get())?.Count > 0;
        //    //    if (isExisted)
        //    //    {
        //    //        return;
        //    //    }

        //    //    string filePath = System.IO.Path.GetFullPath("RawData/parameters.json");
        //    //    using (StreamReader r = new StreamReader(filePath))
        //    //    {
        //    //        string json = r.ReadToEnd();
        //    //        if (json == null)
        //    //        {
        //    //            return;
        //    //        }
        //    //        List<object> parameters = JsonConvert.DeserializeObject<List<object>>(json);
        //    //        if (parameters != null)
        //    //        {
        //    //            foreach (var parameter in parameters)
        //    //            {
        //    //                var param = JObject.Parse(parameter.ToString());
        //    //                await parameterRepository.Add(new ParameterViewModel()
        //    //                {
        //    //                    UnitName = param["Unit"]?.ToString(),
        //    //                    EchoReportParameterId = (int)param["Parameter Key"],
        //    //                    DatabaseName = param["Database Name"]?.ToString(),
        //    //                    ShowInChart = (bool)param["Show In Chart"],
        //    //                    ShowInParameterTable = (bool)param["Show In Parameter Table"],
        //    //                    ShowInAssessmentText = (bool)param["Show In Assessment Text"],
        //    //                    TableFriendlyName = param["Parameter Table Friendly Name"]?.ToString(),
        //    //                    TextFriendlyName = param["Parameter Text Friendly Name"]?.ToString(),
        //    //                    DisplayDecimal = (int)param["Display Decimal"],
        //    //                    Is4D = (bool)param["Parameter Is 4D"],
        //    //                    OrderInAssessment = (int)param["Order In Assessment Text"],
        //    //                    POH = param["Part Of Heart"]?.ToString(),
        //    //                    Priority = (int)param["Parameter Prio"],
        //    //                    ParameterName = param["Parameter Name"]?.ToString(),
        //    //                    SourceUrl = param["Source Url"]?.ToString(),
        //    //                });
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //catch (System.Exception ex)
        //    //{
        //    //    await systemLogRepository.Add(new SystemLog()
        //    //    {
        //    //        Message = ex.Message,
        //    //        Status = SystemLogStatus.INIT_SEED_DATABASE,
        //    //    });
        //    //}
        //}

        //public static async Task SeedReference(IParameterRepository parameterRepository, IReferenceRepository referenceRepository, ISystemLogRepository systemLogRepository)
        //{
        //    try
        //    {
        //        var isExisted = (await referenceRepository.Get())?.Count > 0;
        //        if (isExisted)
        //        {
        //            return;
        //        }

        //        string filePath = System.IO.Path.GetFullPath("RawData/references.json");
        //        using (StreamReader r = new StreamReader(filePath))
        //        {
        //            string json = r.ReadToEnd();
        //            if (json == null)
        //            {
        //                return;
        //            }
        //            List<object> references = JsonConvert.DeserializeObject<List<object>>(json);

        //            List<ParameterViewModel> parameters = (List<ParameterViewModel>)await parameterRepository.QueryAsync();
        //            if (references != null)
        //            {
        //                foreach (var reference in references)
        //                {
        //                    try
        //                    {
        //                        var refer = JObject.Parse(reference.ToString());
        //                        var paramId = (int)refer["Parameter Key"];
        //                        if (paramId != null)
        //                        {
        //                            var parameter = parameters.FirstOrDefault(i => i.EchoReportParameterId == paramId);
        //                            if (parameter != null)
        //                            {
        //                                paramId = parameter.Id;
        //                            }
        //                        }
        //                        await referenceRepository.Add(new Reference()
        //                        {
        //                            Gender = GenderExtension.ConvertFromString((string?)refer["Gender"]),
        //                            AgeFrom = (int?)refer["Age From"],
        //                            AgeTo = (int?)refer["Age To"],
        //                            Min = (decimal?)refer["Min Value"],
        //                            Max = (decimal?)refer["Max Value"],
        //                            Low = (decimal?)refer["Low Value"],
        //                            NormalLower = (decimal?)refer["Normal Lower Limit"],
        //                            NormalUpper = (decimal?)refer["Normal Upper Limit"],
        //                            MildlyLower = (decimal?)refer["Mildly abnormal Lower"],
        //                            MildlyUpper = (decimal?)refer["Mildly abnormal Upper"],
        //                            ModeratelyLower = (decimal?)refer["Moderately abnormal Lower"],
        //                            ModeratelyUpper = (decimal?)refer["Moderately abnormal Upper"],
        //                            SeverelyLimit = (decimal?)refer["Severely abnormal Limit"],
        //                            ParameterId = paramId,
        //                            ParameterName = (string?)refer["Parameter Name"],
        //                            IsCalculated = (string)refer["Is Calculated"] == "1",
        //                            UsedInCode = (string)refer["Used in Code"] == "-1",
        //                            ScanMode = (string?)refer["Scan Mode"],
        //                            View = (string?)refer["View"],
        //                            RaceKey = (int?)refer["Race Key"],
        //                            Unit = (string?)refer["Unit"],
        //                            ToolTip = (string?)refer["ToolTip"],
        //                            PrimaryReference = (string?)refer["Primay Reference"],
        //                            SecondaryReference = (string?)refer["Secondary Reference"],
        //                            Commentary = (string?)refer["Commentary"]
        //                        });
        //                    }
        //                    catch (System.Exception)
        //                    {
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        await systemLogRepository.Add(new SystemLog()
        //        {
        //            Message = ex.Message,
        //            Status = SystemLogStatus.INIT_SEED_DATABASE,
        //        });
        //    }
        //}

        //public static async Task SeedAssessment(IAssessmentRepository assessmentRepo, ISystemLogRepository systemLogRepository)
        //{
        //    try
        //    {
        //        var isExisted = (await assessmentRepo.Get())?.Count > 0;
        //        if (isExisted)
        //        {
        //            return;
        //        }

        //        string filePath = System.IO.Path.GetFullPath("RawData/assessments.json");
        //        using (StreamReader r = new StreamReader(filePath))
        //        {
        //            string json = r.ReadToEnd();
        //            if (json == null)
        //            {
        //                return;
        //            }
        //            List<object> assessments = JsonConvert.DeserializeObject<List<object>>(json);
        //            if (assessments != null)
        //            {
        //                foreach (var assessment in assessments)
        //                {
        //                    var assess = JObject.Parse(assessment.ToString());
        //                    await assessmentRepo.Add(new AssessmentText()
        //                    {
        //                        EchoReportAssessmentTextId = (int)assess["Assessment Key"],
        //                        Level = (int)assess["Assessment Level"],
        //                        CallFunction = (int)assess["Assessment Call Function"],
        //                        Text = (string?)assess["Assessment Text"] ?? "",
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        await systemLogRepository.Add(new SystemLog()
        //        {
        //            Message = ex.Message,
        //            Status = SystemLogStatus.INIT_SEED_DATABASE,
        //        });
        //    }
        //}

        //public static async Task SeedValve(IValveRepository valveRepo, ISystemLogRepository systemLogRepository)
        //{
        //    try
        //    {
        //        var isExisted = (await valveRepo.Get())?.Count > 0;
        //        if (isExisted)
        //        {
        //            return;
        //        }

        //        string filePath = System.IO.Path.GetFullPath("RawData/valves.json");
        //        using (StreamReader r = new StreamReader(filePath))
        //        {
        //            string json = r.ReadToEnd();
        //            if (json == null)
        //            {
        //                return;
        //            }
        //            List<object> valves = JsonConvert.DeserializeObject<List<object>>(json);
        //            if (valves != null)
        //            {
        //                foreach (var valve in valves)
        //                {
        //                    var val = JObject.Parse(valve.ToString());
        //                    await valveRepo.Add(new ValveData()
        //                    {
        //                        Name = (string)val["Valve Name"],
        //                        Value = (string)val["Valve Value"],
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        await systemLogRepository.Add(new SystemLog()
        //        {
        //            Message = ex.Message,
        //            Status = SystemLogStatus.INIT_SEED_DATABASE,
        //        });
        //    }
        //}

        public class FileFolder
        {
            public string File { get; set; }
            public string? Folder { get; set; }
            public string RootPath { get; set; }

        }

        public static async Task SeedPythonCode(IPythonCodeRepository pythonCodeRepo, IPythonDefaultRepository pythonDefaultRepo, ISystemLogRepository systemLogRepository)
        {
            try
            {
                var root = PathExtension.DefaultRoot();

                var constantsFolderDefault = Path.Join(root, "Constants".AsSpan());
                var dtosFolderDefault = Path.Join(root, "DTOs".AsSpan());
                var helpersFolderDefault = Path.Join(root, "Helpers".AsSpan());
                var logicFolderDefault = Path.Join(root, "Logic".AsSpan());

                var executeScriptsFolder = Path.Join(root, "ExecuteScrips".AsSpan());
                if (!Directory.Exists(executeScriptsFolder))
                    Directory.CreateDirectory(executeScriptsFolder);

                var constantsExecuteFolder = Path.Join(executeScriptsFolder, "Constants".AsSpan());
                if (!Directory.Exists(constantsExecuteFolder))
                    Directory.CreateDirectory(constantsExecuteFolder);

                var dtosExecuteFolder = Path.Join(executeScriptsFolder, "DTOs".AsSpan());
                if (!Directory.Exists(dtosExecuteFolder))
                    Directory.CreateDirectory(dtosExecuteFolder);

                var helpersExecuteFolder = Path.Join(executeScriptsFolder, "Helpers".AsSpan());
                if (!Directory.Exists(helpersExecuteFolder))
                    Directory.CreateDirectory(helpersExecuteFolder);

                var logicExecuteFolder = Path.Join(executeScriptsFolder, "Logic".AsSpan());
                if (!Directory.Exists(logicExecuteFolder))
                    Directory.CreateDirectory(logicExecuteFolder);

                var generateAssessment = Path.Join(root, "generate_assessment.py");
                List<FileFolder> fileNameFolders = new List<FileFolder>();
                fileNameFolders.Add(new FileFolder { File = "generate_assessment.py", Folder = null, RootPath = generateAssessment });

                // list path folder default and new default
                List<string> listDefaultFolders = new List<string>()
                {
                    logicFolderDefault,
                    constantsFolderDefault,
                    helpersFolderDefault,
                    dtosFolderDefault
                };
                List<string> listExecuteFolders = new List<string>()
                {
                    logicExecuteFolder,
                    constantsExecuteFolder,
                    helpersExecuteFolder,
                    dtosExecuteFolder
                };

                // get path file python default
                foreach (var it in listDefaultFolders)
                {
                    DirectoryInfo folder = new DirectoryInfo(it); //Assuming is your Folder
                    FileInfo[] Files = folder.GetFiles("*.py"); //Getting python files
                    foreach (FileInfo file in Files)
                    {
                        fileNameFolders.Add(new FileFolder { File = file.Name, Folder = folder.Name, RootPath = file.FullName });
                    }
                }

                var defaultCodes = await pythonCodeRepo.QueryAndSelectAsync(selector: m => m.Path, filter: e => !string.IsNullOrEmpty(e.Path), null, "");

                if (defaultCodes.Count() == 0)
                {
                    for (int i = 0; i < fileNameFolders.Count(); i++)
                    {
                        string defaultContent = File.ReadAllText(fileNameFolders[i].RootPath);

                        var path = "";
                        if (fileNameFolders[i].Folder == "Logic")
                        {
                            path = Path.Join(logicExecuteFolder, fileNameFolders[i].File);
                        }
                        else if (fileNameFolders[i].Folder == "Constants")
                        {
                            path = Path.Join(constantsExecuteFolder, fileNameFolders[i].File);
                        }
                        else if (fileNameFolders[i].Folder == "DTOs")
                        {
                            path = Path.Join(dtosExecuteFolder, fileNameFolders[i].File);
                        }
                        else if (fileNameFolders[i].Folder == "Helpers")
                        {
                            path = Path.Join(helpersExecuteFolder, fileNameFolders[i].File);
                        }
                        else
                        {
                            path = Path.Join(executeScriptsFolder, fileNameFolders[i].File);
                        }

                        if (!File.Exists(path))
                        {
                            using (FileStream fs = File.Create(path))
                            {
                                using (StreamWriter writer = new StreamWriter(fs))
                                {
                                    await writer.WriteAsync(defaultContent);
                                }
                            }
                        }
                        else
                        {
                            using (StreamWriter writer = new StreamWriter(path))
                            {
                                await writer.WriteAsync(defaultContent);
                            }
                        }
                        var PythonDefault = new PythonDefault()
                        {
                            FileName = fileNameFolders[i].File,
                            Script = defaultContent,
                            Path = fileNameFolders[i].RootPath,
                        };
                        PythonDefault.PythonCodes.Add(new PythonCode
                        {
                            Script = defaultContent,
                            IsCurrentVersion = true,
                            IsDefault = true,
                            Path = path,
                            Version = 1
                        });
                        await pythonDefaultRepo.Add(PythonDefault);
                    }
                }
            }
            catch (System.Exception ex)
            {
                await systemLogRepository.Add(new SystemLog()
                {
                    Message = ex.Message,
                    Status = SystemLogStatus.INIT_SEED_DATABASE,
                });
            }
        }
    }
}