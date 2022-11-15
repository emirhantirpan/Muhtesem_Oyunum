using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{

    
    [Header("Oyunun Geneli")]
    [SerializeField] private float skor;

    [Header("Hareket Mekanikleri")]
    [Tooltip("Oyuncunun hız değeridir.")] [SerializeField] private float oyuncununHizi;

    public bool oyuncuEtkilesimeGirebilir = false;

    public GameObject oyuncu;

    
    void Start()
    {
    }

    
    void Update()
    {
        if(oyuncununHizi > 5)
        {
            oyuncu.gameObject.SetActive(false);

            
            StartCoroutine(OyuncununHiziAzalmasi());
        }
    }

   
    IEnumerator OyuncununHiziAzalmasi()
    {
        oyuncu.GetComponent<Player>().movementSpeed = 10f;

        yield return new WaitForSeconds(2f);

        oyuncu.GetComponent<Player>().movementSpeed = 30f;
    }

    private void OnTriggerEnter(Collider col)
    { 
        if (col.gameObject.tag == "ColliderObjesi")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                oyuncununHizi += 20f;
            }
            oyuncuEtkilesimeGirebilir = true; 
        }
    }
   
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "ColliderObjesi")
        {
            oyuncuEtkilesimeGirebilir = false;
        }
    }
}
