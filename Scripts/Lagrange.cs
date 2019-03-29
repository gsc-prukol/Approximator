using UnityEngine;

public class Lagrange : MonoBehaviour
{
   // public int length = 5; // кількість точок з заданими значеннями
    public float[] x;
    public float[] y; //у[i] це значення функції в точці і + 1
    private float[,] a; // коефіцієнти поліномів
    private float[] b; // вектор знаменнників
    private float[] r; // ковектор коефіцієнтів результуючого поліному
    public int poinsDetalizations = 120; //кількість точок лінії при зображенні лінії
    void Awake()
    {
        b = new float[x.Length];
        a = new float[x.Length, x.Length];
        y = new float[x.Length];
        r = new float[x.Length];
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
       // Debug.Log("DrawLine()");
        calculationCoefficients();
        LineRenderer lr = gameObject.GetComponent<LineRenderer>();
         for(int i = 0; i < poinsDetalizations; i++)
         {
             x =  i / 10f ;
             lr.SetPosition(i, new Vector3(2 * x, meaningPolynomial(x)));
         }
       // Debug.Log("2, " + meaningPolynomial(2f));
    }
    float meaningPolynomial(float input)
    {
        float res = r[r.Length - 1];
        float tmp = input * r[0];
        for (int i = 1; i < r.Length - 1; i++)
        {
            tmp += r[i];
            tmp *= input;
        }
        res += tmp;
        return res;
    }
    void calculationCoefficients()
    {
        for (int i = 0; i < r.Length; i++)
        {
            r[i] = 0;
            for (int j = 0; j < r.Length; j++)
            {
                r[i] += a[j, i] * y[j];
            }
        }
       // Debug.Log("R:" + r[0] + ", " + r[1] + ", " + r[2] + ", " + r[3] + ", " + r[4] + "; ");
       // Debug.Log("Y:" + y[0] + ", " + y[1] + ", " + y[2] + ", " + y[3] + ", " + y[4] + "; ");
    }
    void calculationCoefficientsA()
    {
        float[] args = new float[x.Length - 1];
  
        calculationDenominators();
        for (int i = 0; i < args.Length; i++)
        {
            args[i] = x[i];
        }
        for (int i = x.Length - 1; i >= 0; i--)
        {
            if (i != x.Length - 1)
                args[i]++;
            calculationCoefficientsRow(i, args);
            for (int j = 0; j < x.Length; j ++)
            {
                a[i, j] /= b[i];
            }
        }

    }
    void calculationCoefficientsRow(int row, float[] args)
    {
        float[] tmp = { 1f, -args[0] };
        for (int i = 1; i < args.Length; i++)
        {
            tmp = amountPolynom(tmp, args[i]);
        }
        for(int i = 0; i < a.GetLength(1); i++) // 1 вимір - кількість стовпців
        {
            Debug.Log(tmp[i]);
            a[row, i] = tmp[i];
        }
        // Debug.Log("Row" + row + ": (" + a[row, 0] + ", " + a[row, 1] + ", " + a[row, 2] + ", " + a[row, 3] + ", " + a[row, 4] + ")");
    }

    public float[] amountPolynom(float[] polynom, float ai)
    {
        float[] coeffs = new float[polynom.Length + 1];
        float[] polynom2 = { 1f, -ai };
        for (int i = 0; i < polynom.Length; ++i)
            for (int j = 0; j < 2; ++j) // множимо на поліном вигляду х - аі
                coeffs[i + j] += polynom[i] * polynom2[j];
        return coeffs;
    }
    void calculationDenominators()
    {
        for (int i = 0; i < x.Length; i++)
        {
            b[i] = 1f;
            for (int j = 0; j < x.Length; j++)
            {
                if (i != j)
                {
                    b[i] *= x[i] - x[j];
                }
            }
        }
    }
}
