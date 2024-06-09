using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

public class DecryptFunc
{
    private int d_value;
    private int n_value;
    private List<BigInteger> encryptedMessage;
    
    public DecryptFunc(List<BigInteger> encryptedMessage)
    {
        this.d_value = (int)encryptedMessage[0];
        this.n_value = (int)encryptedMessage[encryptedMessage.Count - 1];
        this.encryptedMessage = new List<BigInteger>(encryptedMessage);
    }

    public string Decrypt()
    {
        StringBuilder decryptedMessage = new StringBuilder();
        
        encryptedMessage.RemoveAt(0); 
        encryptedMessage.RemoveAt(encryptedMessage.Count - 1); 
        
        foreach (BigInteger encryptedChar in encryptedMessage)
        {
            int decryptedChar = DecryptChar(encryptedChar);
            decryptedMessage.Append((char)decryptedChar);
        }
        return decryptedMessage.ToString();
    }

    private int DecryptChar(BigInteger encrypted)
    {
        BigInteger bigD = new BigInteger(d_value);
        BigInteger bigN = new BigInteger(n_value);

        BigInteger decrypted = ModPow(encrypted, bigD, bigN);
        return (int)decrypted;
    }

    // ModPow Override
    private BigInteger ModPow(BigInteger baseValue, BigInteger exponent, BigInteger modulus)
    {
        if (modulus == 1) return 0;
        BigInteger result = 1;
        baseValue = baseValue % modulus;
        while (exponent > 0)
        {
            if ((exponent % 2) == 1)
                result = (result * baseValue) % modulus;
            exponent = exponent >> 1;
            baseValue = (baseValue * baseValue) % modulus;
        }
        return result;
    }
}
