using UnityEngine;
using System.Collections;

public class IKFoot : MonoBehaviour {

	public bool useIK;


	//Czy można złapać lewą stopą;
	public bool leftFootIK;
	//Czy można złapać prawą stopą;
	public bool rightFootIK;
	
	//Nowa pozycja dla lewej stopy.
	public Vector3 leftFootPos;
	//Nowa pozycja dla prawej stopy.
	public Vector3 rightFootPos;
	
	//Zmienna przechowująca wynik obrotu lewej stopy (obrót stopy tak aby wyglądala, że na czymś stoji). 
	public Quaternion leftFootRot;
	//Zmienna przechowująca wynik obrotu prawej stopy (obrót stopy tak aby wyglądala, że na czymś stoji).  
	public Quaternion rightFootRot;

	//dodatkowy obrót lewej stopy (sama stopa ustawiana pod odpowiednim kontem).
	public Quaternion leftFootRotOffset;
	//dodatkowy obrót prawej stopy (sama stopa ustawiana pod odpowiednim kontem).
	public Quaternion rightFootRotOffset;
	
	//Dodatkowy odstęp dla lewej stopy (margines)
	public Vector3 leftFootOffset;
	//Dodatkowy odstęp dla lewej stopy (margines)
	public Vector3 rightFootOffset;
	
	//Komponent transformacji.
	public Transform trans;
	//Komponent animatora.
	private Animator anim;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Promień dla lewej stopy.
		RaycastHit LFHit;
		//Promień dla prawej stopy.
		RaycastHit RFHit;

		//Lewa noga IK
		if (Physics.Raycast (trans.position + new Vector3 (-0.4f, 0.4f, 0.0f), trans.forward, out LFHit, 1f)) {
			leftFootIK = true;
			leftFootPos = LFHit.point - leftFootOffset;
			leftFootRot = (Quaternion.FromToRotation(Vector3.up, LFHit.normal)) * leftFootRotOffset;
		} else {
			leftFootIK = false;
		}

		//Prawa noga IK
		if (Physics.Raycast (trans.position + new Vector3 (0.4f, 0.4f, 0.0f), trans.forward, out RFHit, 1f)) {
			rightFootIK = true;
			rightFootPos = RFHit.point - rightFootOffset;
			rightFootRot = (Quaternion.FromToRotation(Vector3.up, RFHit.normal))  * rightFootRotOffset;
		} else {
			rightFootIK = false;
		}

		
	}
	
	// Update is called once per frame
	void Update () {
		//Promień lewej nogi.
		Debug.DrawRay (trans.position + new Vector3(-0.4f, 0.4f, 0.0f), trans.forward, Color.red);

		//Promień prawej nogi.
		Debug.DrawRay (trans.position + new Vector3(0.4f, 0.4f, 0.0f), trans.forward, Color.red);
	}
	
	void OnAnimatorIK(){
		if (useIK) {


			if(leftFootIK) {

				anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos);
				anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
				
				anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRot);
				anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
			}

			if(rightFootIK) {
				anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos);
				anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);

				anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRot);
				anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);
			}

		}
	}
}
