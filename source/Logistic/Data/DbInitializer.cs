using Microsoft.EntityFrameworkCore;

namespace Logistic.Data
{
  public static class DbInitializer
  {
    public static void Initialize(LogisticContext context)
    {
      context.Database.Migrate();
    }
  }
}
