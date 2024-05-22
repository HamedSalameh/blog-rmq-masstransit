namespace MessageProcessingService
{
    internal class ClinicalDataMessage
    {
        required public string PatientId { get; set; }
        required public string Data { get; set; }

        public override string ToString()
        {
            return $"PatientId: {PatientId}, Data: {Data}";
        }
    }
}