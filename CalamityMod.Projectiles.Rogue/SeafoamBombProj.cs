using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SeafoamBombProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 240;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
		base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.15f;
		base.Projectile.velocity.X = base.Projectile.velocity.X * 0.99f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = (base.Projectile.Calamity().stealthStrike ? 256 : 128));
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		for (int i = 0; i < ((!base.Projectile.Calamity().stealthStrike) ? 1 : 5); i++)
		{
			float posX = base.Projectile.Center.X + (float)(base.Projectile.Calamity().stealthStrike ? Main.rand.Next(-50, 51) : 0);
			float posY = base.Projectile.Center.Y + (float)(base.Projectile.Calamity().stealthStrike ? Main.rand.Next(-50, 51) : 0);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), posX, posY, 0f, 0f, ModContent.ProjectileType<SeafoamBubble>(), (int)((double)base.Projectile.damage * 0.4), 0f, base.Projectile.owner);
		}
		if (Main.netMode == 2)
		{
			return;
		}
		Vector2 goreSource = base.Projectile.Center;
		int goreAmt = (base.Projectile.Calamity().stealthStrike ? 6 : 3);
		Vector2 source = default(Vector2);
		((Vector2)(ref source))._002Ector(goreSource.X - 24f, goreSource.Y - 24f);
		for (int goreIndex = 0; goreIndex < goreAmt; goreIndex++)
		{
			float velocityMult = 0.33f;
			if (goreIndex < goreAmt / 3)
			{
				velocityMult = 0.66f;
			}
			if (goreIndex >= 2 * goreAmt / 3)
			{
				velocityMult = 1f;
			}
			ModContent.GetInstance<CalamityMod>();
			int type = Main.rand.Next(61, 64);
			int smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj = Main.gore[smoke];
			obj.velocity *= velocityMult;
			obj.velocity.X++;
			obj.velocity.Y++;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj2 = Main.gore[smoke];
			obj2.velocity *= velocityMult;
			obj2.velocity.X--;
			obj2.velocity.Y++;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj3 = Main.gore[smoke];
			obj3.velocity *= velocityMult;
			obj3.velocity.X++;
			obj3.velocity.Y--;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj4 = Main.gore[smoke];
			obj4.velocity *= velocityMult;
			obj4.velocity.X--;
			obj4.velocity.Y--;
		}
	}
}
