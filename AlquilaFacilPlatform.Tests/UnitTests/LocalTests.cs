using AlquilaFacilPlatform.Locals.Domain.Model.Aggregates;
using AlquilaFacilPlatform.Locals.Domain.Model.Commands;

namespace AlquilaFacilPlatform.Tests.UnitTests;

public class LocalTests
{
    [Fact]
    public void Local_Constructor_WithParameters_ShouldInitializeProperties()
    {
        string district = "Miraflores";
        string street = "Malecon Cisneros";
        string localName = "Casa Urbana";
        string country = "Perú";
        string city = "Lima";
        int price = 125;            // per night
        string photoUrl = "https://a0.muscache.com/im/pictures/pro_photo_tool/Hosting-14127027-unapproved/original/af62b9d0-db54-4d2d-aa89-20cc5a40394f.JPEG?im_w=720";
        string descriptionMessage = "Esta casa urbana combina diseño contemporáneo con comodidades de lujo. Disfruta de espacios amplios, luz natural y una ubicación privilegiada cerca de restaurantes, tiendas y parques.";
        int localCategoryId = 1;
        int userId = 1;
        string features = "Wi-Fi, Baños";
        int capacity = 0;

        var local = new Local(district, street, localName, country, city, price, photoUrl, descriptionMessage,
            localCategoryId, userId,features,capacity);
        
        Assert.Equal(localName, local.LocalName);
        Assert.Equal(district, local.Address.District);
        Assert.Equal(street, local.Address.Street);
        Assert.Equal(price, local.Price.PriceNight);
        Assert.Equal(photoUrl, local.PhotoUrl);
        Assert.Equal(country, local.Place.Country);
        Assert.Equal(city, local.Place.City);
        Assert.Equal(descriptionMessage, local.DescriptionMessage);
        Assert.Equal(localCategoryId, local.LocalCategoryId);
        Assert.Equal(userId, local.UserId);
        Assert.Equal(features, local.Features);
        Assert.Equal(capacity, local.Capacity);
    }

    [Fact]
    public void Local_Create_ShouldCreateLocals()
    {
        var createLocal = new CreateLocalCommand(
            "Miraflores",
            "Malecon Cisneros",
            "Casa Urbana",
            "Perú",
            "Lima",
            125,
            "https://a0.muscache.com/im/pictures/pro_photo_tool/Hosting-14127027-unapproved/original/af62b9d0-db54-4d2d-aa89-20cc5a40394f.JPEG?im_w=720",
            "Esta casa urbana combina diseño contemporáneo con comodidades de lujo. Disfruta de espacios amplios, luz natural y una ubicación privilegiada cerca de restaurantes, tiendas y parques.",
            1,1,"Wi-Fi, Baños",10);
        
        var local = new Local(createLocal);

        Assert.Equal(createLocal.LocalType, local.LocalName);
        Assert.Equal(createLocal.District, local.Address.District);
        Assert.Equal(createLocal.Street, local.Address.Street);
        Assert.Equal(createLocal.Price, local.Price.PriceNight);
        Assert.Equal(createLocal.PhotoUrl, local.PhotoUrl);
        Assert.Equal(createLocal.Country, local.Place.Country);
        Assert.Equal(createLocal.City, local.Place.City);
        Assert.Equal(2, local.LocalCategoryId);    
        Assert.Equal(createLocal.Features, local.Features);
        Assert.Equal(createLocal.Capacity, local.Capacity);
    }
}