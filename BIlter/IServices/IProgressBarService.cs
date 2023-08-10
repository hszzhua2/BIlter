using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.IServices
{
    //先定义接口
    public interface IProgressBarService
    {
        void Start(int maximum);

        void Stop();
    }
}
