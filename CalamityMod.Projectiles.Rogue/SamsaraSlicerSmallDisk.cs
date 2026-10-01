using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SamsaraSlicerSmallDisk : ModProjectile, ILocalizedModType, IModType
{
	public Projectile Parent;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.aiStyle = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.ArmorPenetration = 20;
	}

	public override void AI()
	{
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		if (Parent != null && !Parent.active)
		{
			Parent = null;
		}
		Player player = Main.player[base.Projectile.owner];
		if (base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.localNPCHitCooldown = 3;
			base.Projectile.usesLocalNPCImmunity = true;
		}
		bool returning = false;
		base.Projectile.ai[1]++;
		base.Projectile.rotation += MathHelper.ToRadians(12f);
		float Vel = 15f;
		if (base.Projectile.Calamity().stealthStrike)
		{
			Vel = 20f;
		}
		float length = 10f;
		if (base.Projectile.Calamity().stealthStrike)
		{
			length = 15f;
		}
		if (base.Projectile.ai[1] > length)
		{
			Vel += base.Projectile.ai[1] - length;
			if (Parent != null)
			{
				if (Parent.active)
				{
					base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.DirectionTo(Parent.Center) * Vel, base.Projectile.Calamity().stealthStrike ? 0.25f : 0.35f);
					if ((base.Projectile.Center + base.Projectile.velocity).Distance(Parent.Center) < ((Vector2)(ref base.Projectile.velocity)).Length())
					{
						base.Projectile.Kill();
					}
				}
				else
				{
					returning = true;
				}
			}
			else
			{
				returning = true;
			}
		}
		if (Parent != null && Parent.active)
		{
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(8f));
		}
		if (Parent != null && !returning)
		{
			Projectile projectile = base.Projectile;
			projectile.position += Parent.velocity;
		}
		if (Parent == null)
		{
			base.Projectile.timeLeft = 5;
			Projectile projectile2 = base.Projectile;
			projectile2.position += player.velocity;
			base.Projectile.velocity = base.Projectile.DirectionTo(player.Center) * 4f;
			base.Projectile.scale *= 0.9f;
			if (base.Projectile.Distance(player.Center) < ((Vector2)(ref base.Projectile.velocity)).Length())
			{
				base.Projectile.Kill();
			}
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		Parent = Main.projectile[(int)base.Projectile.ai[0]];
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 1; i < base.Projectile.oldPos.Length; i++)
		{
			Vector2 oldP1 = base.Projectile.oldPos[i];
			Vector2 oldP2 = base.Projectile.oldPos[i - 1];
			base.Projectile.oldPos[i] = oldP2 + oldP2.DirectionTo(oldP1) * MathHelper.Min(oldP2.Distance(oldP1), 5f);
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], new Color(0.2f, 1f, 0f, 0f), 2);
		return false;
	}
}
