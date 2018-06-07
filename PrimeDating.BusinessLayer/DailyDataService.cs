using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.DataAccess.Models;
using PrimeDating.Models;

namespace PrimeDating.BusinessLayer
{
    public class DailyDataService : IDailyDataService
    {
        private static string _cannotConvertToIntMessage = "Cannot convert {0} from '{1}' to integer";
        public void UpdateDailyData(DailyDataDto data)
        {
            Validate(data);
        }

        private void Validate(DailyDataDto data)
        {
            var girls = ValidateAndReturnGirls(data.Girls);
        }

        private List<Girls> ValidateAndReturnGirls(List<GirlDto> girls)
        {
            var validationMessage = new StringBuilder();

            var result = girls.Select(girlDto => new Girls
                {
                    Id = GetIntValueFromDto(girlDto.GirlId, validationMessage,
                        string.Format(_cannotConvertToIntMessage, "girlID", girlDto.GirlId)),
                    AssignedManagerId = GetIntValueFromDto(girlDto.Operator, validationMessage,
                        string.Format(_cannotConvertToIntMessage, "operator", girlDto.Operator)),
                    FirstName = girlDto.Name,
                    Passport = girlDto.Passport,
                    City = girlDto.City,
                    Country = girlDto.Country,
                    Height = GetIntValueFromDto(girlDto.Height, validationMessage,
                        string.Format(_cannotConvertToIntMessage, "height", girlDto.Height)),
                    Weight = GetIntValueFromDto(girlDto.Weight, validationMessage,
                        string.Format(_cannotConvertToIntMessage, "weight", girlDto.Weight)),
                    BodyType = girlDto.BodyType,
                    MartialStatus = girlDto.MartialStatus,
                    Education = girlDto.Education,
                    Religion = girlDto.Religion,
                    Smoking = girlDto.Smoking,
                    Drinking = girlDto.Drinking,
                    WorkPlace = girlDto.Occupation,
                    BirthDay = girlDto.DateBirth,
                    ChildrenCount = GetIntValueFromDto(girlDto.CountChildren, validationMessage,
                        string.Format(_cannotConvertToIntMessage, "countChildren", girlDto.CountChildren)),
                    ColorEye = girlDto.ColorEye,
                    ColorHair = girlDto.ColorHair,
                    LookingFor = girlDto.LookingFor,
                    Description = girlDto.Description,
                    EngLevel = girlDto.EngLevel,
                    OtherLangs = girlDto.OtherLangs,
                    Avatar = girlDto.Avatar,
                    CanReceiveGifts = girlDto.CanRecieveRealGifts
            })
                .ToList();

            if (!string.IsNullOrWhiteSpace(validationMessage.ToString()))
            {
                throw new PrimeDatingException("Girls entities validation error:\r\n" + validationMessage);
            }

            return result;
        }

        private int GetIntValueFromDto(string initValue, StringBuilder validationMessage,
            string errorMessage)
        {
            if (int.TryParse(initValue, out var id))
            {
                return id;
            }

            validationMessage.AppendLine(errorMessage);

            return 0;
        }
    }
}
