using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNK : MonoBehaviour
{
    public int lengs = 5;
    public int power = 4; // кількість коефіцієнтів поліному, степінь + 1
    public int poinsDetalizations = 120; //кількість точок лінії при зображенні лінії
    public float[] y;
    private float[,] a;
    private float[] b;
    private float[] c;
    
    // Start is called before the first frame update
    void Awake()
    {
        a =  new float[,] { { 24.2f,     -30.833333f,   11f,        -1.166666f},           //обернена матриця, хардкод
                            { -30.83333f, 41.349206f,   -15.17857f,  1.6388888f},
                            { 11f,        -15.17857f,    5.69642857f, -0.625f},
                            { -1.166666f,  1.6388888f,    -0.625f,   0.06944444f}};
        b = new float[power];
        y = new float[lengs];
        c = new float[power];
    }
    void Start()
    {
        DrawLine();
    }
    public void DrawLine()
    {
        float x;
        Debug.Log("DrawLine()");
        calculationCoefficientsC();
        LineRenderer lr = gameObject.GetComponent<LineRenderer>();
        for (int i = 0; i < poinsDetalizations; i++)
        {
            x = i / 10f;
            lr.SetPosition(i, new Vector3(2 * x, meaningPolynomial(x)));
        }
        Debug.Log("2, " + meaningPolynomial(2f));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    float meaningPolynomial(float x)
    {
        float res = 0;
        for (int i = 0; i < power; i++)
        {
            res += Mathf.Pow(x, i) * c[i];
        }

        return res;
    }
    void calculationCoefficientsB()
    {
        for (int i = 0; i < power; i++)
        {
            b[i] = 0f;
            for (int j = 0; j < lengs; j++)
            {
                b[i] += y[j] * Mathf.Pow(j + 1, i);
            }
        }
        Debug.Log("B: " + b[0] + ", " + b[1] + ", " + b[2] + ", " + b[3]);
    }
    void calculationCoefficientsC()
    {
        calculationCoefficientsB();
        for (int i = 0; i < power; i++)
        {
            c[i] = 0;
            for (int j = 0; j < power; j++)
            {
                c[i] += a[i, j] * b[j];
            }
        }
        Debug.Log("C: " + c[0] + ", " + c[1] + ", " + c[2] + ", " + c[3]);
    }
}
