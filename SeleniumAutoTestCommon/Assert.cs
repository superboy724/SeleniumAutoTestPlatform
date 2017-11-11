using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutoTestCommon
{
    public class Assert
    {
        public AssestResult Result { get; set; }

        public long UsedTime { get; set; }

        public static Assert Success()
        {
            return new Assert()
            {
                Result = AssestResult.Success
            };
        }

        public static Assert Failed()
        {
            return new Assert()
            {
                Result = AssestResult.Failed
            };
        }

        public static Assert AreEquel(object left,object right)
        {
            if(left == right)
            {
                return new Assert()
                {
                    Result = AssestResult.Success
                };
            }
            else
            {
                return new Assert()
                {
                    Result = AssestResult.Failed
                };
            }
        }

        public static Assert NotEquel(object left, object right)
        {
            if (left == right)
            {
                return new Assert()
                {
                    Result = AssestResult.Failed
                };
            }
            else
            {
                return new Assert()
                {
                    Result = AssestResult.Success
                };
            }
        }

        public static Assert TestError()
        {
            return new Assert()
            {
                Result = AssestResult.Error
            };
        }
    }
}
