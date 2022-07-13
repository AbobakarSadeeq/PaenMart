using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.AppSettingClasses
{
    public class ApplicationSettings
    {
        public string? JWT_Secret { get; set; }
        public string? Client_URL { get; set; }
    }
}
