using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lagrange : MonoBehaviour
{
    public int length = 5; // кількість точок з заданими значеннями
    public float[] y; //у[i] це значення функції в точці і + 1
    private float[,] a; // коефіцієнти поліномів
    private float[] b; // вектор знаменнників
    private float[] r; // ковектор коефіцієнтів результуючого поліному
    public int poinsDetalizations = 120; //кількість точок лінії при зображенні лінії
    void Awake()
    {
        b = new float[length];
        a = new float[length, length];
        y = new float[length];
        r = new float[length];
    }
    // Start is called before the first frame update
    void Start()
    {
        calculationCoefficientsA();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DrawLine()
    {
        float x;
        Debug.Log("DrawLine()");
        calculationCoefficients();
        LineRenderer lr = gameObject.GetComponent<LineRenderer>();
         for(int i = 0; i < poinsDetalizations; i++)
         {
             x =  i / 10f ;
             lr.SetPosition(i, new Vector3(2 * x, meaningPolynomial(x)));
         }
        Debug.Log("2, " + meaningPolynomial(2f));
    }
    float meaningPolynomial(float x)
    {
        float res = 0;
        for (int i = 0; i < length; i++)
        {
            res += Mathf.Pow(x, i) * r[length - i - 1];
        }

        return res;
    }
    void calculationCoefficients()
    {
        for (int i = 0; i < length; i++)
        {
            r[i] = 0;
            for (int j = 0; j < length; j++)
            {
                r[i] += a[j, i] * y[j];
            }
        }
        Debug.Log("R:" + r[0] + ", " + r[1] + ", " + r[2] + ", " + r[3] + ", " + r[4] + "; ");
        Debug.Log("Y:" + y[0] + ", " + y[1] + ", " + y[2] + ", " + y[3] + ", " + y[4] + "; ");
    }
    void calculationCoefficientsA()
    {
        float[] args = new float[length - 1];
        calcukationDenominators();
        for (int i = 0; i < length - 1; i++)
        {
            args[i] = i + 1;
        }
        for (int i = length - 1; i >= 0; i--)
        {
            if (i != length - 1)
                args[i]++;
            calculationCoefficientsRow(i, args);
            for (int j = 0; j < length; j ++)
            {
                a[i, j] /= b[i];
            }
        }

    }
    void calculationCoefficientsRow(int row, float[] args)
    {
        a[row, 0] = 1f;
        a[row, 1] = 0;
        a[row, 2] = (args[0] + args[1]) *(args[2] + args[3]) + args[0] * args[1] + args[2] * args[3];
        a[row, 3] = (args[0] + args[1]) * args[2] * args[3] + (args[2] + args[3]) * args[0] * args[1];
        a[row, 3] *= -1;
        a[row, 4] = 1;

        for (int i = 0; i < args.Length; i++)
        {
            a[row, 1] -= args[i];
            a[row, 4] *= args[i];
        }
        Debug.Log("Row" + row + ": (" + a[row, 0] + ", " + a[row, 1] + ", " + a[row, 2] + ", " + a[row, 3] + ", " + a[row, 4] + ")");
    }
    void calcukationDenominators()
    {
        for (int i = 1; i <= length; i++)
        {
            b[i - 1] = 1f;
            for (int j = 1; j <= length; j++)
            {
                if (i != j)
                {
                    b[i - 1] *= i - j;
                }
            }
        }
    }
}
