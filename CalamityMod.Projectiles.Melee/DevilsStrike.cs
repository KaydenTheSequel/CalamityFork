using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class DevilsStrike : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public float fade;

	public int angleTimer;

	public Color clr;

	public int curveDir;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<DevilsDevastation>();

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.extraUpdates = 80;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 0;
		base.Projectile.timeLeft = 400;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		float deathLerp = Utils.Remap(base.Projectile.timeLeft, 300f, 0f, 40f, 5f);
		if (time == 0)
		{
			if (base.Projectile.ai[0] == 0f)
			{
				base.Projectile.ai[0] = 0.75f;
			}
			angleTimer += Main.rand.Next(60, 81);
			curveDir = (Main.rand.NextBool() ? 1 : (-1));
		}
		time++;
		if (angleTimer > 0)
		{
			angleTimer--;
			base.Projectile.velocity = base.Projectile.velocity.RotatedByRandom(0.009999999776482582);
		}
		else
		{
			angleTimer = Main.rand.Next(60, 81);
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.9f * Utils.GetLerpValue(0f, 300f, base.Projectile.timeLeft, clamped: true) * (float)curveDir * Main.rand.NextFloat(0.85f, 1.15f) * base.Projectile.ai[0]);
			curveDir *= -1;
		}
		if (time % 6 == 0)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.1f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 20, Main.rand.NextFloat(0.05f, 0.055f) * deathLerp * base.Projectile.ai[0], (Main.rand.NextBool() ? Color.MediumOrchid : clr) * (CalamityClientConfig.Instance.Photosensitivity ? 0.25f : 0.7f), new Vector2(0.8f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.2f));
		}
		if (angleTimer % 60 == 0 && base.Projectile.ai[0] > 0.7f)
		{
			if (base.Projectile.ai[1] > 0f)
			{
				base.Projectile.ai[0] -= 0.08f;
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (base.Projectile.velocity * 0.8f).RotatedBy(Main.rand.NextBool() ? (-0.6f) : 0.6f), ModContent.ProjectileType<DevilsStrike>(), base.Projectile.damage, 0f, base.Projectile.owner, base.Projectile.ai[0] * 0.7f).timeLeft = 250;
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		return false;
	}

	public DevilsStrike()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		angleTimer = 20;
		clr = Color.Lerp(Color.DeepPink, Color.Orange, 0.5f);
		curveDir = 1;
		base._002Ector();
	}
}
