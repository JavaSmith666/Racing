using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitrogenControler : MonoBehaviour
{
	private static float minStartSpeed = 2f;
	private static float maxStartSpeed = 5f;
	private static float minRateOverLifetime = 10f;
	static float maxRateOverLifetime = 100f;

	private static NitrogenControler thisNitrogenControler;

	private void Start()
	{
		thisNitrogenControler = this;
	}

	[System.Obsolete]
	public static void UpdateParticleSystem(bool state, float speed = 0f, float minVelocity = 0f,
		float maxVelocity = 0f)
	{
		ParticleSystem left = thisNitrogenControler.transform.Find("LF_Nitrogen").gameObject.
				GetComponent<ParticleSystem>();
		ParticleSystem right = thisNitrogenControler.transform.Find("RF_Nitrogen").gameObject.
				GetComponent<ParticleSystem>();
		if (!state)
		{
			//Left side.
			left.startSpeed = right.startSpeed = 0f;
			left.emissionRate = right.emissionRate = 0f;
		}
		else
		{
			left.startSpeed = right.startSpeed = Mathf.Lerp(minStartSpeed, maxStartSpeed,
				Mathf.InverseLerp(minVelocity, maxVelocity, speed));
			left.emissionRate = right.emissionRate = Mathf.Lerp(minRateOverLifetime, maxRateOverLifetime,
				Mathf.InverseLerp(minVelocity, maxVelocity, speed));
		}
	}
}