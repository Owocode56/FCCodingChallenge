using System.ComponentModel;

namespace FCCodingChallenge.API.Data.Enum
{
    public enum ResponseCode
    {
        [Description("Success")]
        Successful = 00,
        
        [Description("Validation Error")]
        ValidationError = 01,
        
        [Description("Not Found")]
        NotFound = 02,
        
        [Description("Processing Error")]
        ProcessingError = 03,
        
        [Description("Unauthorized Access")]
        AuthorizationError = 04,
        
        [Description("Duplicate Error")]
        DuplicateError = 05,
        
        [Description("Pending")]
        Pending = 06,
        
        [Description("Exception Occurred")]
        Exception = 07,
        
        [Description("Internal Server Error")]
        InternalServer = 08,
        
        [Description("Invalid Request")]
        INVALID_REQUEST = 09
    }
}
