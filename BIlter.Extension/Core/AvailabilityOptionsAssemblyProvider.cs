using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Extension.Core
{
    internal class AvailabilityOptionsAssemblyProvider
    {
        private readonly string _name = "BIlter.AvailabilityOptions";
        private AvailabilityOptionsAssemblyProvider()
        {
            AssemblyName name = new AssemblyName(_name);

            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndCollect);

            ModuleBuilder = assemblyBuilder.DefineDynamicModule(_name, $"{_name}.dll");
        }

        private static AvailabilityOptionsAssemblyProvider _instance;

        public static AvailabilityOptionsAssemblyProvider Current = _instance ??= new AvailabilityOptionsAssemblyProvider();

        public ModuleBuilder ModuleBuilder;

        public TypeBuilder DefineType(string typeFullname)
        {
            return ModuleBuilder.DefineType($"{_name}.{typeFullname}", TypeAttributes.Public | TypeAttributes.Class);
        }
    }
}
