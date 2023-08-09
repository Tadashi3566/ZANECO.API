namespace ZANECO.API.Application.SMS;

public static class ClassSms
{
    public static string FormatContactNumber(string contactNumber)
    {
        if (contactNumber is null) return default!;

        if (contactNumber.Length <= 9) return contactNumber;

        contactNumber = contactNumber.Trim();

        return $"+639{contactNumber[^9..]}";
    }

    public static string RemoveWhiteSpaces(string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray());
    }

    public static string[] GetDistinctFromArray(string recipients)
    {
        recipients = RemoveWhiteSpaces(recipients);
        string[] recipientArray = recipients.Split(',');

        return recipientArray.Distinct().ToArray();
    }
}