using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class CraniumSmasherStealth : ModProjectile, ILocalizedModType, IModType
{
	private NPC StickTarget;

	private Vector2 StickOffset;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Rogue/CraniumSmasherExplosive";

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 300;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 60;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		if (StickTarget != null)
		{
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.Center = StickTarget.Center + StickOffset;
			base.Projectile.tileCollide = false;
			if (!StickTarget.active)
			{
				base.Projectile.Kill();
			}
			return;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 5f)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.02f;
		base.Projectile.velocity.Y += 0.085f;
		base.Projectile.velocity.X *= 0.99f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 300);
		if (StickTarget == null)
		{
			StickTarget = target;
			StickOffset = base.Projectile.Center - target.Center;
			if (base.Projectile.timeLeft < 62)
			{
				base.Projectile.timeLeft = 62;
			}
		}
		if (base.Projectile.penetrate > 1)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<CraniumSMASH>(), base.Projectile.damage, 0f, base.Projectile.owner);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 300);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<CraniumSMASH>(), (int)((float)base.Projectile.damage * 1.5f), 0f, base.Projectile.owner, 0f, 1f);
		}
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		Main.LocalPlayer.SetScreenshake(3.5f);
		if (!Main.dedServ)
		{
			int goreAmt = 3;
			Vector2 source = base.Projectile.Center - new Vector2(24f);
			for (int goreIndex = 1; goreIndex <= goreAmt; goreIndex++)
			{
				float velocityMult = 0.33f * (float)goreIndex;
				int type = Main.rand.Next(61, 64);
				Gore gore = Gore.NewGoreDirect(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				gore.velocity *= velocityMult;
				type = Main.rand.Next(61, 64);
				Gore gore2 = Gore.NewGoreDirect(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				gore2.velocity *= velocityMult;
			}
		}
		for (int i = 0; i < 30; i++)
		{
			float edgeOffset = Main.rand.NextFloat(60f, 100f) * (float)((!Main.rand.NextBool()) ? 1 : (-1));
			float randOffset = Main.rand.NextFloat(-100f, 100f);
			Dust.NewDustPerfect(base.Projectile.Center + ((i % 2 == 0) ? new Vector2(edgeOffset, randOffset) : new Vector2(randOffset, edgeOffset)), 135, Vector2.Zero, 100, default(Color), 2f).noGravity = true;
		}
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/CraniumSmasherGlow", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
	}
}
