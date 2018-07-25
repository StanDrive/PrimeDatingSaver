using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models;
using PrimeDating.Models.Database;

namespace PrimeDating.BusinessLayer
{
    public class DailyDataService : IDailyDataService
    {
        private const string UnnecessaryPaymentType = "0";

        private readonly ILogger _logger;

        private readonly IDataAccessFactory _dataAccessFactory;

        private static string _cannotConvertToIntMessage = "Cannot convert {0} from '{1}' to integer";

        private const string InvalidPropertyValue = "undefined";

        private Dictionary<int, string> _managerRoles;

        private Dictionary<int, string> _giftStatuses;

        private Dictionary<int, string> _paymentTypes;

        private IDictionaryDataService DictionaryDataService => _dataAccessFactory.GetDictionaryDataService();

        private IAdminAreaDataService AdminAreaDataService => _dataAccessFactory.GetAdminAreaDataService();

        private IGirlsDataService GirlsDataService => _dataAccessFactory.GetGirlsDataService();

        private IManagerDataService ManagerDataService => _dataAccessFactory.GetManagerDataService();

        private IMenDataService MenDataService => _dataAccessFactory.GetMenDataService();

        private IOrdersDataService OrdersDataService => _dataAccessFactory.GetOrdersDataService();

        private IGiftsDataService GiftsDataService => _dataAccessFactory.GetGiftsDataService();

        private IPaymentsDataService PaymentsDataService => _dataAccessFactory.GetPaymentsDataService();

        public DailyDataService(ILogger logger, IDataAccessFactory dataAccessFactory)
        {
            _logger = logger;

            _dataAccessFactory = dataAccessFactory;
        }

        public void UpdateDailyData(DailyDataDto data)
        {
            var dailyData = ValidateAndReturnDailyDataEntities(data);

            UpdateManagersData(dailyData.Managers, dailyData.AdminAreaId);

            UpdateGirlsData(dailyData.Girls, dailyData.AdminAreaId);

            UpdateGirlsPassportScans(dailyData.GirlsPassportScans);

            UpdateGirlsImages(dailyData.GirlImages);

            UpdateManagersGirlsReference(dailyData.ManagersGirls);

            UpdateMenData(dailyData.Men);

            UpdateOrdersData(dailyData.Orders);

            UpdateGiftsData(dailyData.Gifts);

            UpdateGiftOrdersData(dailyData.GiftOrders);

            UpdatePaymentsData(dailyData.Payments);
        }

        private void UpdatePaymentsData(List<Payments> payments)
        {
            PaymentsDataService.AddOrUpdatePayments(payments);
        }

        private void UpdateGiftOrdersData(List<GiftOrders> giftOrders)
        {
            GiftsDataService.AddOrUpdateGiftOrders(giftOrders);
        }

        private void UpdateGiftsData(List<Gifts> gifts)
        {
            GiftsDataService.AddOrUpdateGifts(gifts);
        }

        private void UpdateOrdersData(List<Orders> orders)
        {
            OrdersDataService.AddOrUpdateOrders(orders);
        }

        private void UpdateMenData(List<Men> men)
        {
            MenDataService.AddOrUpdateMen(men);
        }

        private void UpdateManagersGirlsReference(List<ManagersGirls> managersGirls)
        {
            GirlsDataService.AddOrUpdateManagersGirlsReference(managersGirls);
        }

        private void UpdateGirlsImages(List<GirlsImages> girlImages)
        {
            foreach (var girlGroup in girlImages.GroupBy(t => t.GirlId))
            {
                GirlsDataService.AddOrUpdateImages(girlGroup.Key, girlGroup.Select(t => t.Url).ToList());
            }
        }

        private void UpdateGirlsPassportScans(List<GirlsPassportScans> passportScans)
        {
            foreach (var girlGroup in passportScans.GroupBy(t => t.GirlId))
            {
                GirlsDataService.AddOrUpdatePassportScans(girlGroup.Key, girlGroup.Select(t => t.Url).ToList());
            }
        }

        private void UpdateManagersData(List<Managers> managers, int adminAreaId)
        {
            foreach (var manager in managers)
            {
                manager.AdminAreaId = adminAreaId;

                ManagerDataService.AddOrUpdateManager(manager);
            }
        }

        private void UpdateGirlsData(List<Girls> girls, int adminAreaId)
        {
            foreach (var girl in girls)
            {
                girl.AdminAreaId = adminAreaId;

                GirlsDataService.AddOrUpdateGirl(girl);
            }
        }

        private DailyData ValidateAndReturnDailyDataEntities(DailyDataDto data)
        {
            var adminAreaId = GetAdminAreaId(data.Account);

            var result = new DailyData
            {
                AdminAreaId = adminAreaId,
                Girls = ValidateAndReturnGirls(data.Girls),
                Managers = ValidateAndReturnManagers(data.Managers),
                Gifts = ValidateAndReturnGifts(data.Gifts, adminAreaId),
                Men = ValidateAndReturnMen(data.Men),
                Payments = ValidateAndReturnPayments(data.PaymentsStatistic, adminAreaId),
                GirlsPassportScans = ValidateAndReturnGirlsPassportScans(data.Girls),
                GirlImages = ValidateAndReturnGirlsImages(data.Girls),
                ManagersGirls = GetManagersGirlsReference(data.Girls),
                Orders = ValidateAndReturnOrders(data.Gifts),
                GiftOrders = ValidateAndReturnGiftOrders(data.Gifts)
            };

            return result;
        }

        private List<GiftOrders> ValidateAndReturnGiftOrders(List<GiftDto> gifts)
        {
            var validationMessage = new StringBuilder();

            if (gifts == null || !gifts.Any())
            {
                return new List<GiftOrders>();
            }

            var giftOrders = (from gift in gifts
                from giftOrder in gift.Orders
                select new GiftOrders
                {
                    GiftId = int.Parse(gift.GiftId),
                    OrderId = int.Parse(giftOrder.OrderId),
                    Amount = GetDecimal(giftOrder.OrderAmount, validationMessage),
                    Price = GetDecimal(giftOrder.OrderPrice, validationMessage)
                }).ToList();

            if (!string.IsNullOrWhiteSpace(validationMessage.ToString()))
            {
                throw new PrimeDatingException("GiftOrders entities validation error:\r\n" + validationMessage);
            }

            return giftOrders;
        }

        private List<Orders> ValidateAndReturnOrders(List<GiftDto> gifts)
        {
            var orders = new List<Orders>();

            if (gifts == null || !gifts.Any())
            {
                return new List<Orders>();
            }

            foreach (var order in gifts.SelectMany(t => t.Orders))
            {
                if (!orders.Exists(t => t.Id == int.Parse(order.OrderId)))
                {
                    orders.Add(new Orders{Id = int.Parse(order.OrderId), Name = order.OrderName});
                }
            }

            return orders;
        }

        private List<ManagersGirls> GetManagersGirlsReference(List<GirlDto> girls)
        {
            return girls
                .Select(t => new ManagersGirls {GirlId = int.Parse(t.GirlId), ManagerId = int.Parse(t.Operator)})
                .ToList();
        }

        private List<GirlsImages> ValidateAndReturnGirlsImages(List<GirlDto> girls)
        {
            var validationMessage = new StringBuilder();

            var result = (from girlDto in girls
                from passportScan in girlDto.Images
                select new GirlsImages
                {
                    GirlId = GetIntValueFromDto(girlDto.GirlId, validationMessage, string.Format(_cannotConvertToIntMessage, "id", girlDto.GirlId)),
                    Url = passportScan
                }).ToList();

            if (!string.IsNullOrWhiteSpace(validationMessage.ToString()))
            {
                throw new PrimeDatingException("Girl images entities validation error:\r\n" + validationMessage);
            }

            return result;
        }

        private List<GirlsPassportScans> ValidateAndReturnGirlsPassportScans(List<GirlDto> girls)
        {
            var validationMessage = new StringBuilder();

            var result = (from girlDto in girls
                from passportScan in girlDto.PassportScans
                select new GirlsPassportScans
                {
                    GirlId = GetIntValueFromDto(girlDto.GirlId, validationMessage, string.Format(_cannotConvertToIntMessage, "id", girlDto.GirlId)),
                    Url = passportScan
                }).ToList();

            if (!string.IsNullOrWhiteSpace(validationMessage.ToString()))
            {
                throw new PrimeDatingException("Girl passport entities validation error:\r\n" + validationMessage);
            }

            return result;
        }

        private int GetAdminAreaId(string adminAreaName)
        {
            var adminArea = AdminAreaDataService.GetAdminAreaByName(adminAreaName);

            return adminArea?.Id ?? AdminAreaDataService.CreateAdminArea(adminAreaName).Id;
        }

        private List<Payments> ValidateAndReturnPayments(List<PaymentsStatisticDto> payments, int adminAreaId)
        {
            var validationMessage = new StringBuilder();

            if (payments == null || !payments.Any())
            {
                throw new PrimeDatingException("Payments entities validation error: Payments data is empty");
            }

            var result = (from payment in payments
                from paymentStatistic in payment.Statistic.Where(t => t.Key != UnnecessaryPaymentType)
                from paymentStatisticType in paymentStatistic.Value
                select new Payments
                {
                    GirlId = GetIntValueFromDto(payment.Id, validationMessage,
                        string.Format(_cannotConvertToIntMessage, "id", payment.Id)),
                    Date = GetDate(paymentStatisticType.Date, validationMessage) ?? throw new PrimeDatingException($"Can't parse date from '{paymentStatisticType.Date}'"),
                    Amount = GetDecimal(paymentStatisticType.Bonuses, validationMessage),
                    ManagerId = GetIntValueFromDto(paymentStatisticType.Id, validationMessage,
                        string.Format(_cannotConvertToIntMessage, "id", paymentStatisticType.Id)),
                    PaymentTypeId = GetPaymentTypeId(paymentStatistic.Key, validationMessage),
                    AdminAreaId = adminAreaId
                }).ToList();

            if (!string.IsNullOrWhiteSpace(validationMessage.ToString()))
            {
                throw new PrimeDatingException("PaymentsStatistics entities validation error:\r\n" + validationMessage);
            }

            return result;
        }

        private int GetPaymentTypeId(string paymentTypeValue, StringBuilder validationMessage)
        {
            if (_paymentTypes == null || !_paymentTypes.Any())
            {
                _paymentTypes = DictionaryDataService.GetPaymentTypes();
            }

            foreach (var paymentType in _paymentTypes)
            {
                if (string.Equals(paymentType.Value, paymentTypeValue, StringComparison.CurrentCultureIgnoreCase))
                {
                    return paymentType.Key;
                }
            }

            validationMessage.AppendLine($"There is no payment type '{paymentTypeValue}' in DB");

            return -1;
        }

        private static decimal GetDecimal(string value, StringBuilder validationMessage)
        {
            if (decimal.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out var result))
            {
                return result;
            }

            validationMessage.AppendLine($"Unable to get decimal from value '{value}'");

            return 0;
        }

        private List<Men> ValidateAndReturnMen(List<MenDto> men)
        {
            var validationMessage = new StringBuilder();

            if (men == null || !men.Any())
            {
                return new List<Men>();
            }

            var result = men.Select(menDto => new Men
            {
                FirstName = CheckStringPropertyValidity(menDto.Name, validationMessage, "name"),
                Location = CheckStringPropertyValidity(menDto.Location, validationMessage, "location"),
                BirthDay = GetDate(menDto.BirthDate, validationMessage),
                MartialStatus = CheckStringPropertyValidity(menDto.MartialStatus, validationMessage, "martialStatus"),
                Children = CheckStringPropertyValidity(menDto.Children, validationMessage, "children"),
                Religion = CheckStringPropertyValidity(menDto.Religion, validationMessage, "religion"),
                Education = CheckStringPropertyValidity(menDto.Education, validationMessage, "education"),
                WorkPlace = CheckStringPropertyValidity(menDto.Occupation, validationMessage, "occupation"),
                Drinking = CheckStringPropertyValidity(menDto.Drinking, validationMessage, "drinking"),
                Smoking = CheckStringPropertyValidity(menDto.Smoking, validationMessage, "smoking"),
                Id = GetIntValueFromDto(menDto.Id, validationMessage,
                    string.Format(_cannotConvertToIntMessage, "id", menDto.Id))
            }).ToList();

            if (!string.IsNullOrWhiteSpace(validationMessage.ToString()))
            {
                throw new PrimeDatingException("Men entities validation error:\r\n" + validationMessage);
            }

            return result;
        }

        private DateTime? GetDate(string date, StringBuilder validationMessage)
        {
            date = date.Replace("-0001", "1900");

            if (DateTime.TryParse(date, out var result))
            {
                return result;
            }

            if (int.TryParse(date, out var year))
            {
                if (year >= 1900 && year <= DateTime.Now.Year)
                {
                    return new DateTime(year, 1, 1);
                }
            }

            validationMessage.AppendLine($"Unable to get date from value '{date}'");

            return null;
        }

        private List<Gifts> ValidateAndReturnGifts(List<GiftDto> gifts, int adminAreaId)
        {
            var validationMessage = new StringBuilder();

            if (gifts == null || !gifts.Any())
            {
                return new List<Gifts>();
            }

            var result = gifts.Select(giftDto => new Gifts
            {
                GiftLink = CheckStringPropertyValidity(giftDto.GiftLink, validationMessage, "giftLink"),
                Id = GetIntValueFromDto(giftDto.GiftId, validationMessage, string.Format(_cannotConvertToIntMessage, "id", giftDto.GiftId)),
                ManagerId = GetIntValueFromDto(giftDto.OperatorId, validationMessage, string.Format(_cannotConvertToIntMessage, "operatorID", giftDto.OperatorId)),
                ManId = GetIntValueFromDto(giftDto.MaleId, validationMessage, string.Format(_cannotConvertToIntMessage, "maleID", giftDto.MaleId)),
                GirlId = GetIntValueFromDto(giftDto.FemaleId, validationMessage, string.Format(_cannotConvertToIntMessage, "femaleID", giftDto.FemaleId)),
                GiftStatusUpdateDate = giftDto.GiftStatusUpdateDate,
                GiftStatusId = GetGiftStatusId(giftDto.GiftStatus, validationMessage),
                AdminAreaId = adminAreaId
            }).ToList();

            if (!string.IsNullOrWhiteSpace(validationMessage.ToString()))
            {
                throw new PrimeDatingException("Gifts entities validation error:\r\n" + validationMessage);
            }

            return result;
        }

        private int GetGiftStatusId(string status, StringBuilder validationMessage)
        {
            if (_giftStatuses == null || !_giftStatuses.Any())
            {
                _giftStatuses = DictionaryDataService.GetGiftStatuses();
            }

            foreach (var giftStatus in _giftStatuses)
            {
                if (string.Equals(giftStatus.Value, status, StringComparison.CurrentCultureIgnoreCase))
                {
                    return giftStatus.Key;
                }
            }

            validationMessage.AppendLine($"There is no gift status '{status}' in DB");

            return -1;
        }

        private List<Managers> ValidateAndReturnManagers(List<ManagerDto> managers)
        {
            var validationMessage = new StringBuilder();

            if (managers == null || !managers.Any())
            {
                throw new PrimeDatingException("Managers entities validation error: managers data is empty");
            }

            var result = managers.Select(managerDto => new Managers
            {
                Id = GetIntValueFromDto(managerDto.Id, validationMessage,
                    string.Format(_cannotConvertToIntMessage, "id", managerDto.Id)),
                Email = CheckStringPropertyValidity(managerDto.Email, validationMessage, "email"),
                RoleId = GetRoleId(managerDto.Role, validationMessage),
                FirstName = CheckStringPropertyValidity(managerDto.FirstName, validationMessage, "firstname"),
                LastName = CheckStringPropertyValidity(managerDto.LastName, validationMessage, "lastname")
            }).ToList();

            if (!string.IsNullOrWhiteSpace(validationMessage.ToString()))
            {
                throw new PrimeDatingException("Managers entities validation error:\r\n" + validationMessage);
            }

            return result;
        }

        private int GetRoleId(string managerDtoRole, StringBuilder validationMessage)
        {
            if (_managerRoles == null || !_managerRoles.Any())
            {
                _managerRoles = DictionaryDataService.GetManagerRoles();
            }

            foreach (var role in _managerRoles)
            {
                if (string.Equals(role.Value, managerDtoRole, StringComparison.CurrentCultureIgnoreCase))
                {
                    return role.Key;
                }
            }

            validationMessage.AppendLine($"There is no manager role '{managerDtoRole}' in DB");

            return -1;
        }

        private static List<Girls> ValidateAndReturnGirls(List<GirlDto> girls)
        {
            var validationMessage = new StringBuilder();

            if (girls == null || !girls.Any())
            {
                throw new PrimeDatingException("Girls entities validation error: Girls data is empty");
            }
            
            var result = girls.Select(girlDto => new Girls
                {
                    Id = GetIntValueFromDto(girlDto.GirlId, validationMessage,
                        string.Format(_cannotConvertToIntMessage, "girlID", girlDto.GirlId)),
                    AssignedManagerId = GetIntValueFromDto(girlDto.Operator, validationMessage,
                        string.Format(_cannotConvertToIntMessage, "operator", girlDto.Operator)),
                    FirstName = CheckStringPropertyValidity(girlDto.Name, validationMessage, "name"),
                    Passport = CheckStringPropertyValidity(girlDto.Passport?.Replace(" ", string.Empty), validationMessage, "passport"),
                    City = CheckStringPropertyValidity(girlDto.City, validationMessage, "city"),
                    Country = CheckStringPropertyValidity(girlDto.Country, validationMessage, "country"),
                    Height = GetIntValueFromDto(girlDto.Height, validationMessage,
                        string.Format(_cannotConvertToIntMessage, "height", girlDto.Height)),
                    Weight = GetIntValueFromDto(girlDto.Weight, validationMessage,
                        string.Format(_cannotConvertToIntMessage, "weight", girlDto.Weight)),
                    BodyType = CheckStringPropertyValidity(girlDto.BodyType, validationMessage, "bodytype"),
                    MartialStatus =
                        CheckStringPropertyValidity(girlDto.MartialStatus, validationMessage, "martialstatus"),
                    Education = CheckStringPropertyValidity(girlDto.Education, validationMessage, "education"),
                    Religion = CheckStringPropertyValidity(girlDto.Religion, validationMessage, "religion"),
                    Smoking = CheckStringPropertyValidity(girlDto.Smoking, validationMessage, "smoking"),
                    Drinking = CheckStringPropertyValidity(girlDto.Drinking, validationMessage, "drinking"),
                    WorkPlace = CheckStringPropertyValidity(girlDto.Occupation, validationMessage, "occupation"),
                    BirthDay = girlDto.DateBirth,
                    ChildrenCount = GetIntValueFromDto(girlDto.CountChildren, validationMessage,
                        string.Format(_cannotConvertToIntMessage, "countChildren", girlDto.CountChildren)),
                    ColorEye = CheckStringPropertyValidity(girlDto.ColorEye, validationMessage, "coloreye"),
                    ColorHair = CheckStringPropertyValidity(girlDto.ColorHair, validationMessage, "colorhair"),
                    LookingFor = CheckStringPropertyValidity(girlDto.LookingFor, validationMessage, "lookingfor"),
                    Description = CheckStringPropertyValidity(girlDto.Description, validationMessage, "description"),
                    EngLevel = CheckStringPropertyValidity(girlDto.EngLevel, validationMessage, "englevel"),
                    OtherLangs = CheckStringPropertyValidity(girlDto.OtherLangs, validationMessage, "otherlangs"),
                    Avatar = CheckStringPropertyValidity(girlDto.Avatar, validationMessage, "avatar"),
                    CanReceiveGifts = girlDto.CanRecieveRealGifts
                })
                .ToList();

            if (!string.IsNullOrWhiteSpace(validationMessage.ToString()))
            {
                throw new PrimeDatingException("Girls entities validation error:\r\n" + validationMessage);
            }

            return result;
        }

        private static string CheckStringPropertyValidity(string value, StringBuilder validationMessage,
            string propertyName)
        {
            if (!string.Equals(value, InvalidPropertyValue, StringComparison.CurrentCultureIgnoreCase))
            {
                return value;
            }

            validationMessage.AppendLine($"Property {propertyName} contain 'undefined' value");

            return string.Empty;
        }

        private static int GetIntValueFromDto(string initValue, StringBuilder validationMessage,
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
