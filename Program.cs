using System;


class oper
{
    byte[] number;
    int size;

    public oper(string inputnumber)
    {
        size = inputnumber.Length;
        number = new byte[size];

        for (int i = size - 1; i >= 0; i--)
        {
            if (inputnumber[i] >= '0' && inputnumber[i] <= '9')
                number[size - i - 1] = byte.Parse(Convert.ToString(inputnumber[i]));
        }

    }

    public oper(int size)
    {

        number = new byte[size];
        this.size = size;

        for (int i = 0; i < size; i++)
            number[i] = 0;

    }


    public void show()
    {
        for (int i = size - 1; i >= 0; i--)
            Console.Write(number[i]);
        Console.WriteLine();
    }

    public static void show(oper A)
    {
        for (int i = A.size - 1; i >= 0; i--)
            Console.Write(A.number[i]);
        Console.WriteLine();
    }

    public static oper operator *(oper A, oper B)
    {
        int P;
        int timeres;
        int i, j;

        oper C = new oper(A.size + B.size);

        if (A.size == 1 && A.number[0] == 0) { C.size = 1; C.number[0] = 0; return C; }
        if (B.size == 1 && B.number[0] == 0) { C.size = 1; C.number[0] = 0; return C; }
        for (i = 0; i < A.size; i++)
        {
            P = 0;
            for (j = 0; j < B.size; j++)
            {
                timeres = A.number[i] * B.number[j] + P + C.number[i + j];
                C.number[i + j] = (byte)(timeres % 10);
                P = timeres / 10;
            }
            C.number[i + j] = (byte)P;
        }
        int k = C.size - 1;
        while (C.number[k] == 0)
        { C.size -= 1; k--; }
        return C;
    }

    public static oper operator +(oper A, oper B)
    {
        int length;
        int bigsize;
        if (A.size < B.size) { length = A.size; bigsize = B.size; }
        else { length = B.size; bigsize = A.size; }
        oper C = new oper(bigsize + 1);
        C.size--;
        int P = 0;
        int res;

        for (int i = 0; i < length; i++)
        {
            res = A.number[i] + B.number[i] + P;
            C.number[i] = (byte)(res % 10);
            P = res / 10;
        }

        if (A.size > B.size)
            for (int i = length; i < bigsize; i++)
                C.number[i] = A.number[i];
        else
            for (int i = length; i < bigsize; i++)
                C.number[i] = B.number[i];

        if (bigsize == length)
            if (P > 0)
            { C.number[length] += (byte)P; C.size += 1; }
            else
            if (P > 0)
                C.number[length] += (byte)P;
        return C;
    }

    public static oper operator -(oper A, oper B)
    {

        oper C = new oper(A.size);
        for (int i = 0; i < C.size; i++) C.number[i] = A.number[i];
        if (A == B) { C.number[0] = 0; C.size = 1; return C; }
        if (A < B) { return (B - A); }


        for (int i = 0; i < B.size; i++)
        {
            if (C.number[i] >= B.number[i])
                C.number[i] = (byte)(C.number[i] - B.number[i]);
            else
            {
                C.number[i + 1]--;
                C.number[i] += 10;
                C.number[i] = (byte)(C.number[i] - B.number[i]);
            }
        }
        int k = C.size - 1;
        while (C.number[k] == 0)
        { C.size -= 1; k--; }
        return C;
    }

    public static oper operator -(oper A, int B)
    {
        oper C = new oper(Convert.ToString(B));
        return (A - C);
    }

    public static oper operator /(oper A, oper B)
    {
        int k = 0;
        while (A >= B)
        { A = A - B; k++; }
        string newk;
        newk = Convert.ToString(k);
        oper C = new oper(newk);
        return C;
    }

    public static bool operator ==(oper A, oper B)
    {
        if (A.size != B.size) return false;

        for (int i = 0; i < A.size; i++)
            if (A.number[i] != B.number[i]) return false;

        return true;
    }

    public static bool operator !=(oper A, oper B)
    {
        if (A.size != B.size) return true;

        for (int i = 0; i < A.size; i++)
            if (A.number[i] != B.number[i]) return true;

        return false;
    }

    public static bool operator >(oper A, oper B)
    {
        if (A.size > B.size)
            return true;
        if (A.size < B.size)
            return false;
        for (int i = A.size - 1; i >= 0; i--)
        {
            if (A.number[i] > B.number[i])
                return true;
            if (A.number[i] < B.number[i])
                return false;
        }
        return false;
    }
    public static bool operator >=(oper A, oper B)
    {
        if (A.size > B.size)
            return true;
        if (A.size < B.size)
            return false;
        for (int i = A.size - 1; i >= 0; i--)
        {
            if (A.number[i] > B.number[i])
                return true;
            if (A.number[i] < B.number[i])
                return false;
        }
        return true;
    }
    public static bool operator <(oper A, oper B)
    {
        if (A.size > B.size)
            return false;
        if (A.size < B.size)
            return true;
        for (int i = A.size - 1; i >= 0; i--)
        {
            if (A.number[i] > B.number[i])
                return false;
            if (A.number[i] < B.number[i])
                return true;
        }
        return false;
    }

    public static bool operator <=(oper A, oper B)
    {
        if (A.size > B.size)
            return false;
        if (A.size < B.size)
            return true;
        for (int i = A.size - 1; i >= 0; i--)
        {
            if (A.number[i] > B.number[i])
                return false;
            if (A.number[i] < B.number[i])
                return true;
        }
        return true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        oper res = new oper("1");
        oper osn = new oper("2");
        oper x = new oper("1");
        for (int i = 0; i < 64; i++)
        {
            res = res * osn;
        }
        res = res - x;
        oper.show(res);
    }
}
