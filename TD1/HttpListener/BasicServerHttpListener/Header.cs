using System;

public class Header
{

    private System.Collections.Specialized.NameValueCollection headers;

    public Header(System.Collections.Specialized.NameValueCollection headers)
    {
        this.headers = headers;
    }

	public void afficher()
    {
        foreach (String key in headers.AllKeys)
        {
            string[] values = headers.GetValues(key);
            if (values.Length > 0)
            {
                Console.WriteLine("The values of the {0} header are: ", key);
                foreach (string value in values)
                {
                    Console.WriteLine("   {0}", value);
                }
            }
            else
            {
                Console.WriteLine("There is no value associated with the header.");
            }
        }
    }
}
