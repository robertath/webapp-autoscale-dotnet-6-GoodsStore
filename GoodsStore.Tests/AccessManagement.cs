using AutoMapper;
using GoodsStore.App.Models.AccessManagement;
using GoodsStore.App.Models.AccessManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStore.Tests
{
    public class AccessManagement
    {
        [Fact]
        public void MapperMenuRegistrationViewToMenu()
        {
            //Arrange
            var config = new MapperConfiguration(cfg => {
                //cfg.CreateMap<string, Boolean>().ConvertUsing(new BooleanTypeConverter());
                cfg.CreateMap<MenuRegistrationViewModel, Menu>().ForMember(u => u.Enabled, opt => opt.MapFrom(x => x.Status == "Active" ? true : false));
            });
            //config.AssertConfigurationIsValid();

            var source = new MenuRegistrationViewModel
            {
                Title = "5",
                Label = "asdasd",
                Action = "AutoMapperSamples.GlobalTypeConverters.GlobalTypeConverters+Destination",
                Sequence = 1,
                DtRegistered = DateTime.Now,
                Status = "Active"
            };

            //Act
            IMapper _mapper = new Mapper(config);
            Menu result = _mapper.Map<MenuRegistrationViewModel, Menu>(source);

            //Assert
            Assert.Equal(result.Enabled, true);            
        }

        [Fact]
        public void MapperSimpleExample()
        {
            //Arrange
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<string, Boolean>().ConvertUsing(new BooleanTypeConverter());
                cfg.CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));
                cfg.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
                cfg.CreateMap<string, Type>().ConvertUsing<TypeTypeConverter>();
                cfg.CreateMap<Source, Destination>();
            });
            config.AssertConfigurationIsValid();

            var source = new Source
            {
                Value1 = "5",
                Value2 = "01/01/2000",
                Value3 = "AutoMapperSamples.GlobalTypeConverters.GlobalTypeConverters+Destination",
                Status = "Active"
            };

            //Act
            IMapper _mapper = new Mapper(config);
            Destination result = _mapper.Map<Source, Destination>(source);

            //Assert
            Assert.Equal(result.Status, true);
        }
    }
}
