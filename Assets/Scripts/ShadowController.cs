using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class ShadowController : MonoBehaviour
{
    private Material myMaterial;
    private float myScale;
    private Color resetColor;
    private Color alphaColor;
    private Texture2D texture;
    private Vector2 goalLight;
    private float goalLightRange;
    // Start is called before the first frame update

    private void Awake()
    {
        texture = new Texture2D(1024, 1024, TextureFormat.ARGB32, false);
        resetColor = new Color(0.15f, 0.15f, 0.15f, 1f);
        alphaColor = new Color(0, 0, 0, 0f);
        Color[] resetColorArray = texture.GetPixels();

        for (int i = 0; i < resetColorArray.Length; i++)
        {
            resetColorArray[i] = resetColor;
        }

        texture.SetPixels(resetColorArray);
        texture.Apply();

        myMaterial = GetComponent<MeshRenderer>().material;
        myMaterial.mainTexture = texture;
    }

    void Start()
    {
        myScale = transform.localScale.x;
    }

    public void SetGoalLight(Vector2 textCoord, float range) {
        goalLight = textCoord;
        goalLightRange = range;
        SetUnit(goalLight, goalLightRange);
    }

    public void ReescanGoal() {
        SetUnit(goalLight, goalLightRange);
    }

    public void SetUnit(Vector2 textureCoord, float rangeLight) {

        Vector2 tP = new Vector2(textureCoord.x*1024,textureCoord.y*1024);
        int delta = 0;
        int r = Mathf.RoundToInt(17* rangeLight);
        float r2 = Mathf.Pow(r, 2);
        for (int j = 0; j <= r; j++)
        {
            int xL = Mathf.RoundToInt(Mathf.Sqrt(r2 - Mathf.Pow(j, 2)));
            for (int i = -xL; i <= xL; i++)
            {
                if (tP.x + i < texture.width && tP.y + j < texture.height && tP.x + i >= 0)
                {
                    texture.SetPixel((int)tP.x + i, (int)tP.y + j, alphaColor);
                }
                if (tP.x + i < texture.width && tP.y - j >= 0 && tP.x + i >= 0)
                {
                    texture.SetPixel((int)tP.x + i, (int)tP.y - j, alphaColor);
                }
            }
            delta++;
        }
        texture.Apply();
        //Debug.Log("Change textures"+ tP.ToString());
    }

    public void RemoveUnit(Vector2 textureCoord, float rangeLight)
    {
        Vector2 tP = new Vector2(textureCoord.x * 1024, textureCoord.y * 1024);
        int delta = 0;
        int r = Mathf.RoundToInt(17 * rangeLight);
        float r2 = Mathf.Pow(r, 2);
        for (int j = 0; j <= r; j++)
        {
            int xL = Mathf.RoundToInt(Mathf.Sqrt(r2 - Mathf.Pow(j, 2)));
            for (int i = -xL; i <= xL; i++)
            {
                if (tP.x + i < texture.width && tP.y + j < texture.height && tP.x + i >= 0)
                {
                    texture.SetPixel((int)tP.x + i, (int)tP.y + j, resetColor);
                }
                if (tP.x + i < texture.width && tP.y - j >= 0 && tP.x + i >= 0)
                {
                    texture.SetPixel((int)tP.x + i, (int)tP.y - j, resetColor);
                }
            }
            delta++;
        }
        texture.Apply();
    }



}*/



public class ShadowController : MonoBehaviour
{
    private Material myMaterial;
    private float myScale;
    private Vector2 goalLight;
    private float goalLightRange;

    public Vector4[] _ArrayPoints;
    public float[] _ArrayRange;
    public float[] _ArrayRangeInitial;
    public float[] _ArrayTime;
    public float[] _ArrayRealRange;
    public int _ArrayLength;
    private bool hasChange;

    private float velLerp = 2;
    // Start is called before the first frame update

    private void Awake()
    {
        /*texture = new Texture2D(1024, 1024, TextureFormat.ARGB32, false);
        resetColor = new Color(0.15f, 0.15f, 0.15f, 1f);
        alphaColor = new Color(0, 0, 0, 0f);
        Color[] resetColorArray = texture.GetPixels();

        for (int i = 0; i < resetColorArray.Length; i++)
        {
            resetColorArray[i] = resetColor;
        }

        texture.SetPixels(resetColorArray);
        texture.Apply();*/

        myMaterial = GetComponent<MeshRenderer>().material;
        _ArrayPoints = new Vector4[50];
        _ArrayRange = new float[50];
        _ArrayTime = new float[50];
        _ArrayRangeInitial = new float[50];
        _ArrayRealRange = new float[50];
        _ArrayLength = 0;
    }

    void Start()
    {

        myScale = transform.localScale.x;
        hasChange = false;
    }

    void Update()
    {

        for(int i = 0; i < _ArrayLength; i++) {
            if (_ArrayTime[i] < 1) { 
                _ArrayTime[i] += velLerp * Time.deltaTime;
                if (_ArrayTime[i] > 1) {
                    _ArrayTime[i] = 1;
                }
                _ArrayRange[i] = Mathf.Lerp(_ArrayRangeInitial[i], _ArrayRealRange[i], _ArrayTime[i]);

                hasChange = true;

            }
        }

        if (hasChange) {
            myMaterial.SetVectorArray("_ArrayPoints", _ArrayPoints);
            myMaterial.SetFloatArray("_ArrayRange", _ArrayRange);
            myMaterial.SetInt("_ArrayLength", _ArrayLength);
            hasChange = false;
        }
        

    }

    public void SetGoalLight(Vector2 textCoord, float range)
    {
        goalLight = textCoord;
        goalLightRange = range;
        SetUnit(goalLight, goalLightRange);
    }

    public void ReescanGoal()
    {
        SetUnit(goalLight, goalLightRange);
    }

    public void SetUnit(Vector2 textureCoord, float rangeLight)
    {
        Debug.Log(textureCoord+" "+ rangeLight);
        //Añadimos valores a los array
        _ArrayPoints[_ArrayLength] = new Vector4(textureCoord.x,textureCoord.y,0,0);
        _ArrayRange[_ArrayLength] = rangeLight/2;
        _ArrayTime[_ArrayLength] = 0;
        _ArrayRangeInitial[_ArrayLength] = rangeLight / 2;
        _ArrayRealRange[_ArrayLength] = rangeLight;
        Debug.Log(_ArrayPoints[_ArrayLength].ToString()+"  "+ _ArrayRealRange[_ArrayLength]+"   "+ _ArrayRange[_ArrayLength] + " " + rangeLight);
        _ArrayLength++;

    }

    public void RemoveUnit(Vector2 textureCoord, float rangeLight)
    {
        //Buscamos en los array los valores similares del vector2, obtenemos el ai y eliminamos en los otros puntos
        //Tambien movemos de lugar los valores en el array para no dejar espacios vacios

    }



}

