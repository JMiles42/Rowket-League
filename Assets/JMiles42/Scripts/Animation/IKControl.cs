using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKControl: MonoBehaviour
{
	protected Animator  animator;
	public    bool      rightHandIkActive;
	public    Transform rightHandObj;
	public    bool      leftHandIkActive;
	public    Transform leftHandObj;
	public    bool      headIkActive;
	public    Transform lookObj;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	//a callback for calculating IK
	private void OnAnimatorIK()
	{
		if(animator)
		{
			// Set the look target position, if one has been assigned
			if((lookObj != null) && headIkActive)
			{
				animator.SetLookAtWeight(1);
				animator.SetLookAtPosition(lookObj.position);
			}
			else
				animator.SetLookAtWeight(0);

			// Set the right hand target position and rotation, if one has been assigned
			if((rightHandObj != null) && rightHandIkActive)
			{
				animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
				animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
				animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
				animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
			}
			else
			{
				animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
				animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
			}

			// Set the right hand target position and rotation, if one has been assigned
			if((leftHandObj != null) && leftHandIkActive)
			{
				animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
				animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
				animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
				animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
			}
			else
			{
				animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
				animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
			}
		}
	}
}