using System;
using CalamityMod.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class CountermeasurePalmBlast : ModProjectile, ILocalizedModType, IModType
{
	public bool onSpawn = true;

	private NPC targeted;

	public int lifetime = 18;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public float fade => (float)Math.Pow(Utils.GetLerpValue(0f, 13f, base.Projectile.timeLeft, clamped: true), 4.0);

	public override void SetDefaults()
	{
		base.Projectile.width = 150;
		base.Projectile.height = 150;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = lifetime;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref ArsenalEffects.ArsenalLaserColor)).ToVector3() * 0.5f);
		Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (base.Projectile.ai[1] != -5f)
		{
			targeted = Main.npc[(int)base.Projectile.ai[1]];
		}
		if (targeted == null || !targeted.active)
		{
			targeted = base.Projectile.Center.ClosestNPCAt(150f);
			base.Projectile.ai[1] = -5f;
		}
		if (time == 0f)
		{
			Vector2 launchVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
			float launchPower = 25f;
			targeted.MoveNPC(launchVel, launchPower, ignoreKBImmune: true);
		}
		time++;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (target != targeted)
		{
			return false;
		}
		return null;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		modifiers.SetCrit();
		float critDamage = Math.Min(Main.player[base.Projectile.owner].GetTotalCritChance(base.Projectile.DamageType) * 0.01f, 1f);
		float minMult = 0.25f;
		int hitsToMinMult = 7;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult + critDamage;
		Vector2 launchVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
		float launchPower = 40f;
		target.MoveNPC(launchVel, launchPower, ignoreKBImmune: true);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Texture2D beam = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineFade", (AssetRequestMode)2).Value;
		Texture2D bloom = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float completion = Utils.GetLerpValue(lifetime, 0f, time);
		for (int i = 0; i < 6; i++)
		{
			Color val = Color.Lerp(ArsenalEffects.ArsenalLaserColor, Color.White, (float)i * 0.04f);
			((Color)(ref val)).A = 0;
			Color orbColor = val * 0.8f;
			float scale = base.Projectile.scale * (0.05f + (float)i * 0.01f) * 3f;
			Main.EntitySpriteDraw(beam, base.Projectile.Center - Main.screenPosition, null, orbColor, base.Projectile.rotation, new Vector2((float)(beam.Width / 2), (float)beam.Height), new Vector2(0.3f * fade, 2f) * scale, (SpriteEffects)0);
			Main.EntitySpriteDraw(bloom, base.Projectile.Center - Main.screenPosition, null, orbColor, base.Projectile.rotation + (float)Math.PI / 2f, bloom.Size() * 0.5f, new Vector2(0.5f, 1f + 2f * completion) * scale * 3f * fade, (SpriteEffects)0);
		}
		return false;
	}
}
