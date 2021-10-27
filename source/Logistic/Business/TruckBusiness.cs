using Logistic.DbModels;
using System;

namespace Logistic.Business
{
  public static class TruckBusiness
  {
    public static void InsertUpdateValidation(this Truck truck)
    {
      if (!ManufacturedYearValidation(truck.ManufacturedYear))
        throw new Exception("ManufacturedYearValidation");
      if (!ModelYearValidation(truck.ModelYear))
        throw new Exception("ModelYearValidation");
    }


    public static bool ManufacturedYearValidation(int year)
    {
      return year == DateTime.Today.Year;
    }

    public static bool ModelYearValidation(int year)
    {
      return year == DateTime.Today.Year || year == DateTime.Today.Year + 1;
    }

  }
}
