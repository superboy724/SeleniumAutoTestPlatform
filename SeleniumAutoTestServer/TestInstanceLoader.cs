using SeleniumAutoTestCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutoTestServer
{
    //测试用例加载器
    public class TestInstanceLoader
    {
        public string DllDir { get; set; }


        public TestInstance Load()
        {
            try
            {
                Assembly assembly = Assembly.LoadFile(DllDir);
                var dllTypes = assembly.GetExportedTypes();
                var instanceClassType = dllTypes.Where(s => s.FullName.Split('.')[1] == "Test").FirstOrDefault();
                if(instanceClassType == null)
                {
                    throw new NotSupportedException("This dll is cannot load TestInstance");
                }
                else
                {
                    var instanceClass = (TestInstance)assembly.CreateInstance(instanceClassType.FullName);
                    return instanceClass;
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
