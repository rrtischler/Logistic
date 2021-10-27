using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;

namespace Logistic.DbModels
{
  public class Truck
  {
    public int Id { get; set; }
    public TruckModel Model { get; set; }
    [DisplayName("Manufactured Year")]
    [Remote(action: "VerifyManufacturedYear", controller:"Trucks")]
    public int ManufacturedYear { get; set; }
    [DisplayName("Model Year")]
    [Remote(action: "VerifyModelYear", controller: "Trucks")]
    public int ModelYear { get; set; }
  }

  public enum TruckModel
  {
    [Description("FH")]
    FH,
    [Description("FM")]
    FM
  }
}

