using System.Collections.Generic;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MantisClawJet : ModProjectile, ILocalizedModType, IModType
{
	private int TimerCap = 70;

	public static Color WaterColor;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Particles/ThunderBolt";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 40;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 60;
		base.Projectile.timeLeft = TimerCap;
		base.Projectile.tileCollide = false;
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 5;
		base.Projectile.DamageType = DamageClass.Melee;
	}

	public override void OnSpawn(IEntitySource source)
	{
		base.Projectile.ai[1] = 40f;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.97f;
		if (base.Projectile.ai[0] < 16f)
		{
			base.Projectile.ai[1] -= 2f;
		}
		base.Projectile.ai[0]++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		_ = Main.player[base.Projectile.owner];
		modifiers.SetCrit();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 6; i++)
		{
			float ii = (float)i / 6f;
			Vector2 position = Vector2.Lerp(base.Projectile.position, base.Projectile.oldPos[5], ii);
			float Prog = MathHelper.Lerp(3f, 1f, (float)base.Projectile.timeLeft / (float)TimerCap);
			Gore gore = Gore.NewGorePerfect(base.Projectile.GetSource_FromAI(), position, base.Projectile.velocity * 0.2f + Main.rand.NextVector2Circular(1f, 1f) * Prog, 411);
			gore.timeLeft = 8 + Main.rand.Next(6);
			gore.scale = Main.rand.NextFloat(0.6f, 1f) * (1f + Prog * 0.4f);
			gore.type = (Main.rand.NextBool(3) ? 412 : 411);
		}
		for (int j = 0; j < 3; j++)
		{
			float ii2 = (float)j / 3f;
			GeneralParticleHandler.SpawnParticle(new WaterFoamParticle(Vector2.Lerp(base.Projectile.position, base.Projectile.oldPos[5], ii2), scale: MathHelper.Lerp(3f, 10f, (float)base.Projectile.timeLeft / (float)TimerCap), velocity: base.Projectile.velocity * 0.4f, lifetime: 10 - j * 2, color: WaterColor));
		}
		ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		List<Vector2> positions = new List<Vector2>();
		for (int k = 0; k < base.Projectile.oldPos.Length; k++)
		{
			positions.Add(base.Projectile.oldPos[k]);
		}
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(positions, new PrimitiveSettings((float AA, Vector2 _) => base.Projectile.ai[1], delegate(float CC, Vector2 _)
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			return Lighting.GetColor(Vector2.Lerp(base.Projectile.oldPos[base.Projectile.oldPos.Length - 1], base.Projectile.position, CC).ToPoint()).MultiplyRGBA(WaterColor);
		}, null, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]));
		return false;
	}

	static MantisClawJet()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		WaterColor = new Color(114, 197, 255, 0);
	}
}
