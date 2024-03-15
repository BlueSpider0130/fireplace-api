﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamingCommunityApi.Core.Enums
{
    public enum ErrorName
    {
        // Internal server error
        INTERNAL_SERVER,

        // Main business logic errors
        BAD_REQUEST,
        AUTHENTICATION_FAILED,
        OLD_PASSWORD_NOT_CORRECT,
        EMAIL_ACTIVATION_CODE_NOT_CORRECT,


        // Access Denied and Existance errors
        ACCESS_DENIED,

        USER_ID_EXISTS,
        USER_ID_DOES_NOT_EXIST_OR_ACCESS_DENIED,
        
        USERNAME_EXISTS,
        USERNAME_DOES_NOT_EXIST_OR_ACCESS_DENIED,
        
        EMAIL_ADDRESS_EXISTS,
        EMAIL_ADDRESS_DOES_NOT_EXIST_OR_ACCESS_DENIED,
        
        EMAIL_ID_EXISTS,
        EMAIL_ID_DOES_NOT_EXIST_OR_ACCESS_DENIED,
        
        ERROR_CODE_DOES_NOT_EXIST_OR_ACCESS_DENIED,
        
        ACCESS_TOKEN_VALUE_DOES_NOT_EXIST_OR_ACCESS_DENIED,

        SESSION_ID_DOES_NOT_EXIST_OR_ACCESS_DENIED,


        // Format errors
        REQUEST_CONTENT_TYPE_IS_NOT_VALID,
        REQUEST_BODY_IS_NOT_JSON,
        PASSWORD_MIN_LENGTH,
        PASSWORD_MAX_LENGTH,
        PASSWORD_AN_UPPERCASE_LETTER,
        PASSWORD_A_NUMBER,
        PASSWORD_A_LOWERCASE_LETTER,
        PASSWORD_VALID_CHARACTERS,

        USERNAME_MIN_LENGTH,
        USERNAME_MAX_LENGTH,
        USERNAME_WRONG_START,
        USERNAME_WRONG_END,
        USERNAME_INVALID_CONSECUTIVE,
        USERNAME_VALID_CHARACTERS,

        EMAIL_ADDRESS_NOT_VALID,

        FIRST_NAME_NOT_VALID,
        LAST_NAME_NOT_VALID,

        ERROR_CLIENT_MESSAGE_NOT_VALID,

        ACCESS_TOKEN_VALUE_IS_NOT_VALID,


        // Missing parameter(s) errors
        FIRST_NAME_IS_NULL,
        LAST_NAME_IS_NULL,
        USERNAME_IS_NULL,
        PASSWORD_IS_NULL,
        EMAIL_ADDRESS_IS_NULL,
        USER_ID_IS_NULL,
        REQUIRED_BOTH_OF_OLD_PASSWORD_AND_PASSWORD,
        EMAIL_ID_IS_NULL,
        ACTIVATION_CODE_IS_NULL,
        ERROR_CODE_IS_NULL,
        ACCESS_TOKEN_VALUE_IS_NULL,
        SESSION_ID_IS_NULL,
    }
}
