namespace DAL.Infrastructure
{
    public static class Validations
    {
        public static void ValidateDataForExistence<T>(T data)
        {
            if (data != null)
                throw new ValidationException("Data already exist", nameof(data));
        }

        public static void ValidateDataForAbsence<T>(T data)
        {
            if (data is null)
                throw new ValidationException("Data does not exist", nameof(data));
        }
    }
}
