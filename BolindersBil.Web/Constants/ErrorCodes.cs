using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Constants
{
    public enum ErrorCodes
    {
        ApiKeyExhausted,
        ApiKeyMissing,
        ApiKeyInvalid,
        ApiKeyDisabled,
        ParametersMissing,
        ParametersIncompatible,
        ParameterInvalid,
        RateLimited,
        RequestTimeout,
        SourcesTooMany,
        SourceDoesNotExist,
        SourceUnavailableSortedBy,
        SourceTemporarilyUnavailable,
        UnexpectedError,
        UnknownError
    }
}
