//    using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SmartSensors.Service.CustomValidationAttribute
//{
//    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
//    public sealed class SensorRangeValidation:ValidationAttribute
//    {
//        public override bool IsValid(object value)
//        {
//            if (value != null)
//            {
                
//                if (_birthJoin > DateTime.Now)
//                {
//                    return new ValidationResult("Birth date can not be greater than current date.");
//                }
//            }
//            return ValidationResult.Success;
//        }
//    }
//}

