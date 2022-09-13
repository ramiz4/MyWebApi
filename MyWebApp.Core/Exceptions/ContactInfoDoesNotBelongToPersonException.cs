﻿namespace MyWebApp.Core.Exceptions
{
    public sealed class ContactInfoDoesNotBelongToPersonException : BadRequestException
    {
        public ContactInfoDoesNotBelongToPersonException(Guid personId, Guid contactInfoId) 
            : base($"The contact info with the identifier {contactInfoId}, dow not belong to the person with the idetifier {personId}")
        {
        }
    }
}
