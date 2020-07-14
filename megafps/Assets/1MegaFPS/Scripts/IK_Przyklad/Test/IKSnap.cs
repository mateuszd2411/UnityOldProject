using UnityEngine;
using System.Collections;

public class IKSnap : MonoBehaviour {

	public bool useIK;
	//Czy można złapać lewą ręką;
	public bool leftHandIK;
	//Czy można złapać prawą ręką;
	public bool rightHandIK;

	//Nowa pozycja dla lewej ręki.
	public Vector3 leftHandPos;
	//Nowa pozycja dla prawej ręki.
	public Vector3 rightHandPos;

	//Prechowuje orginalną pozycję lewej ręki.
	public Vector3 leftHandOrginalPos;
	//Prechowuje orginalną pozycję prawej ręki.
	public Vector3 rightHandOrginalPos;

	//Zmienna dla wykonania obrotu lewą ręką (obrót ręki tak aby wyglądala, że coś chwyta). 
	public Quaternion leftHandRot;
	//Zmienna dla wykonania obrotu prawą ręką (obrót ręki tak aby wyglądala, że coś chwyta). 
	public Quaternion rightHandRot;

	//Dodatkowe przesunięcie dla lewej ręki (margines)
	public Vector3 leftHandOffset;
	//Dodatkowe przesunięcie dla prawej ręki (margines)
	public Vector3 rightHandOffset;

	//Komponent transformacji.
	public Transform trans;
	//Komponent animatora.
	private Animator anim;

	private IKFoot ikFoot;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		trans = GetComponent<Transform>();
		ikFoot = GetComponent<IKFoot> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Promień dla lewej ręki.
		RaycastHit LHit;
		//Promień dla prawej ręki.
		RaycastHit RHit;

		//Promień dla Lewej ręki.
		if (Physics.Raycast (trans.position + new Vector3 (0.0f, 2.0f, 0.5f), -trans.up + new Vector3 (-0.5f, 0.0f, 0.0f), out LHit, 1f)) {
			leftHandIK = true;//Ręka może złapać.
			leftHandPos = LHit.point - leftHandOffset;//Pobranie nowej pozycji dla dłoni.

			leftHandPos.x = leftHandOrginalPos.x - leftHandOffset.x; //Ustawienie pozycji dłoni na osi X.
			leftHandPos.z = ikFoot.leftFootPos.z - leftHandOffset.z;//Ustawienie dłoni na osi Z względem pozycji stopy lewej. 

			//Wykonanie obrotu ręki.
			leftHandRot = Quaternion.FromToRotation(Vector3.forward, LHit.normal);
		} else {
			leftHandIK = false;//Nie ma nic do złapania (poza zasięgiem).
		}

		//Promień dla Prawej ręki.
		if (Physics.Raycast (trans.position + new Vector3 (0.0f, 2.0f, 0.5f), -trans.up + new Vector3 (0.5f, 0.0f, 0.0f), out RHit, 1f)) {
			rightHandIK = true;//Ręka może złapać.
			rightHandPos = RHit.point - rightHandOffset;//Pobranie nowej pozycji dla dłoni.

			//rightHandPos.x = rightHandOrginalPos.x;
			rightHandPos.z = ikFoot.rightFootPos.z - rightHandOffset.z;//Ustawienie dłoni na osi Z względem pozycji stopy prawej. 

			//Wykonanie obrotu ręki.
			rightHandRot = Quaternion.FromToRotation(Vector3.forward, RHit.normal);
		} else {
			rightHandIK = false;//Nie ma nic do złapania (poza zasięgiem).
		}

	}

	// Update is called once per frame
	void Update () {
		//Promień IK dla lewej ręki.
		Debug.DrawRay (transform.position + new Vector3(0, 2, 0.5f), -transform.up + new Vector3(-0.5f, 0.0f, 0.0f), Color.green);
		//Promień IK dla prawej ręki.
		Debug.DrawRay (transform.position + new Vector3(0, 2, 0.5f), -transform.up + new Vector3(0.5f, 0.0f, 0.0f), Color.green);
	}

	void OnAnimatorIK(){
		if (useIK) {

			leftHandOrginalPos = anim.GetIKPosition(AvatarIKGoal.LeftHand);
			rightHandOrginalPos = anim.GetIKPosition(AvatarIKGoal.RightHand);


			if(leftHandIK) {
				anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
				anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPos);

				anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRot);
				anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
			}

			if(rightHandIK) {
				anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
				anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandPos);

				anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandRot);
				anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
			}
		}
	}
}
