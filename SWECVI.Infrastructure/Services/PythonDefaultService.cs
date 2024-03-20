using SWECVI.ApplicationCore.ViewModels;
using SWECVI.ApplicationCore.Interfaces;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore;
using Microsoft.Extensions.Logging;
using SWECVI.ApplicationCore.Common;
using SWECVI.ApplicationCore.Interfaces.Services;

namespace SWECVI.Infrastructure.Services
{
    public class PythonDefaultService : IPythonDefaultService
    {
        private readonly IPythonDefaultRepository _pythonDefaultRepository;
        public PythonDefaultService(IPythonDefaultRepository pythonDefaultRepository)
        {
            _pythonDefaultRepository = pythonDefaultRepository;
        }
    }
}