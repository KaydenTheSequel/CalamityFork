using System.Linq;
using Microsoft.Xna.Framework;

namespace CalamityMod.InverseKinematics;

public class LimbCollection
{
	public Limb[] Limbs;

	public IInverseKinematicsUpdateRule UpdateRule;

	public Vector2 ConnectPoint
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return Limbs.First().ConnectPoint;
		}
	}

	public Vector2 EndPoint
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return Limbs.Last().EndPoint;
		}
	}

	public double TotalLength => Limbs.Sum((Limb l) => l.Length);

	public Limb this[int index]
	{
		get
		{
			return Limbs[index];
		}
		set
		{
			Limbs[index] = value;
		}
	}

	public LimbCollection(IInverseKinematicsUpdateRule updateRule, int limbCount, float limbLength)
	{
		UpdateRule = updateRule;
		Limbs = new Limb[limbCount];
		for (int i = 0; i < limbCount; i++)
		{
			Limbs[i] = new Limb(0f, limbLength);
		}
	}

	public LimbCollection(IInverseKinematicsUpdateRule updateRule, params float[] limbLengths)
	{
		UpdateRule = updateRule;
		int limbCount = limbLengths.Length;
		Limbs = new Limb[limbCount];
		for (int i = 0; i < limbCount; i++)
		{
			Limbs[i] = new Limb(0f, limbLengths[i]);
		}
	}

	public void UpdateConnectPoints(Vector2? connectPoint = null)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (connectPoint.HasValue)
		{
			Limbs[0].ConnectPoint = connectPoint.Value;
		}
		for (int i = 1; i < Limbs.Length; i++)
		{
			Limbs[i].ConnectPoint = Limbs[i - 1].EndPoint;
		}
	}

	public void Update(Vector2 connectPoint, Vector2 destination)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		UpdateRule.Update(this, destination);
		Limbs[0].ConnectPoint = connectPoint;
		UpdateConnectPoints(connectPoint);
	}
}
