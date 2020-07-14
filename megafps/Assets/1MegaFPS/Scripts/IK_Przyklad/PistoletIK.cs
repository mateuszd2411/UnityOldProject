using UnityEngine;
using System.Collections;

/**
 * Skrypt pozwala wpłynąć na pozycję dłoni modelu. 
 * Wykorzystywany do ustawienia dłoni w pozycji odpowiedniej dla pozycji trzymania pistoletu.
 *
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class PistoletIK : MonoBehaviour {  

    //Component animatora.
    private Animator anim;    

    /** Obiekt z pozycję do jakiej ma dążyć dłoń.*/
    public GameObject pozycjaDlaDloni;
    /** Obiekt będący częścią dłoni, miejsce w którym ma zostać umieszczona broń.*/
    public GameObject miejsceWDloni;
    /** Model broni.*/
    public GameObject modelBroni;

    /** Wektory pozwalające na utworzenie quaternionu w celu korekty obrotu wycelowanego pistoletu.*/
    public Vector3 rotacjaOffset1 = new Vector3(0.5f, -0.8f, 1);
    public Vector3 rotacjaOffset2 = new Vector3(1.2f, 3f, -1f);

    // Use this for initialization
    void Start() {       
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() {      

        //Ustawiam pozycję modelu pistoletu na taką jaką zawiera dłoń.
        modelBroni.GetComponent<Transform>().position = miejsceWDloni.GetComponent<Transform>().position;

        //Tworzę quaterniona z korekcją obrotu pistoletu.        
        Quaternion pistoletRotacjaOffset = Quaternion.FromToRotation(rotacjaOffset1, rotacjaOffset2);
        modelBroni.GetComponent<Transform>().rotation = pozycjaDlaDloni.GetComponent<Transform>().rotation * pistoletRotacjaOffset;
               
    }

    void OnAnimatorIK() {
        //Ustawiam pozycję dłoni na taką jaką zawiera obiekt "pozycjaDlaDloni".
        anim.SetIKPosition(AvatarIKGoal.RightHand, pozycjaDlaDloni.GetComponent<Transform>().position);
        //Ustawiam wagę zmiany pozycji.
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.3f);        

        //Ustawiam obrót dłoni.
        anim.SetIKRotation(AvatarIKGoal.RightHand, pozycjaDlaDloni.GetComponent<Transform>().rotation);
        //Ustawiam wagę obrotu dłoni.
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);       

    }
}
