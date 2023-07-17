using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BIlter.Extension.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Extension.Core
{
    internal static class AvailabilityOptionsFactory
    {
        private static void CreateIL(AvailabilityAttribute attribute, ILGenerator generator)
        {
            generator.Emit(OpCodes.Nop);
            if (attribute.Availability.HasFlag(Data.AvailabilityMode.Always))
            {
                generator.Emit(OpCodes.Ldc_I4_1);
                return;
            }

            if (attribute.Availability.HasFlag(Data.AvailabilityMode.OnlyDocument))
            {

            }

            if (attribute.Availability.HasFlag(Data.AvailabilityMode.OnlyFamily))
            {

            }

            if (attribute.Availability.HasFlag(Data.AvailabilityMode.OnlyProject))
            {

            }

            if (attribute.Availability.HasFlag(Data.AvailabilityMode.OnlyThreeDView))
            {

            }

            if (attribute.Availability.HasFlag(Data.AvailabilityMode.OnlyPlanView))
            {

            }
        }

        public static Type CreateAvailabilityOptions(Type type)
        {
            TypeBuilder typeBuilder = AvailabilityOptionsAssemblyProvider.Current.DefineType($"{type.Name}Availability");

            typeBuilder.AddInterfaceImplementation(typeof(IExternalCommandAvailability));

            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                "IsCommandAvailable",
                MethodAttributes.Public | MethodAttributes.Virtual,
                typeof(bool),
                new Type[2] { typeof(UIApplication), typeof(CategorySet) });

            ILGenerator generator = methodBuilder.GetILGenerator();
            CreateIL(type.GetCustomAttribute<AvailabilityAttribute>(), generator);
            generator.Emit(OpCodes.Ret);

            return typeBuilder.CreateType();
        }
    }
}

