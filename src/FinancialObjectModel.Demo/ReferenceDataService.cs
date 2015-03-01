using FinancialObjectModel.Interfaces;

namespace FinancialObjectModel.Demo
{
    class ReferenceDataService : IReferenceDataService
    {
        public Security GetSecurity(string ticker)
        {
            return default(Security);
        }
    }
}