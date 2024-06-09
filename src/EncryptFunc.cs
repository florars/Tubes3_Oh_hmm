using System;
using System.Collections.Generic;
using System.Numerics;

public class EncryptFunc
{
    private int n_value;
    private int e_value;
    private int d_value;
    private List<int> prime = new List<int>
    {
        1009, 1013, 1019, 1021, 1031, 1033, 1039, 1049, 1051, 1061, 1063, 1069, 1087, 1091, 1093, 1097, 1103, 1109, 1117, 1123, 1129, 1151, 1153, 1163, 1171, 1181, 1187, 1193, 1201, 1213, 1217, 1223, 1229, 1231, 1237, 1249, 1259, 1277, 1279, 1283, 1289, 1297, 1301, 1303, 1307
    };

    private int GenerateRandomNum()
    {
        Random rnd = new Random();
        int randomNumber = rnd.Next(0, prime.Count);
        return randomNumber;
    }

    private int getNumberAt(int idx)
    {
        return prime[idx];
    }

    private int FindGCD(int a, int b)
    {
        if (b == 0)
            return a;
        return FindGCD(b, a % b);
    }

    public void GetKey()
    {
        int idx_p = GenerateRandomNum();
        int idx_q = GenerateRandomNum();
        
        while (idx_p == idx_q)
        {
            idx_p = GenerateRandomNum();
        }

        int p = prime[idx_p];
        int q = prime[idx_q];
        int n = p * q;
        int m = (p - 1) * (q - 1);

        n_value = n;

        int e = prime[GenerateRandomNum()];
        while (FindGCD(e, m) != 1){
            e = prime[GenerateRandomNum()];
        }

        e_value = e;
        d_value = ModInverse(e, m);
    }

    private int ModInverse(int e, int m)
    {
        int m0 = m, t, q;
        int x0 = 0, x1 = 1;

        if (m == 1)
          return 0;

        while (e > 1)
        {
            q = e / m;
            t = m;

            m = e % m;
            e = t;
            t = x0;

            x0 = x1 - q * x0;
            x1 = t;
        }

        if (x1 < 0)
           x1 += m0;

        return x1;
    }

    public List<BigInteger> Encrypt(string message)
    {
        List<BigInteger> encryptedMessage = new List<BigInteger>();
        encryptedMessage.Add(new BigInteger(d_value));
        foreach (char c in message)
        {
            BigInteger encryptedChar = EncryptChar((int)c);
            encryptedMessage.Add(encryptedChar);
        }
        encryptedMessage.Add(new BigInteger(n_value));
        return encryptedMessage;
    }

    private BigInteger EncryptChar(int message)
    {
        BigInteger bigMessage = new BigInteger(message);
        BigInteger bigE = new BigInteger(e_value);
        BigInteger bigN = new BigInteger(n_value);

        BigInteger encrypted = this.ModPow(bigMessage, bigE, bigN);
        return encrypted;
    }

    private BigInteger ModPow(BigInteger baseValue, BigInteger exponent, BigInteger modulus)
    {
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

    public int GetN() => n_value;
    public int GetD() => d_value;
    public int GetE() => e_value;
}
