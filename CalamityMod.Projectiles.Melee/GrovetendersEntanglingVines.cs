using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class GrovetendersEntanglingVines : ModProjectile, ILocalizedModType, IModType
{
	private const float curvature = 16f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/BrokenBiomeBlade_GrovetendersTouchChain";

	public Player Owner => Main.player[base.Projectile.owner];

	public float Timer => 20 - base.Projectile.timeLeft;

	public NPC NPCfrom
	{
		get
		{
			return Main.npc[(int)base.Projectile.ai[0]];
		}
		set
		{
			base.Projectile.ai[0] = value.whoAmI;
		}
	}

	public NPC Target
	{
		get
		{
			return Main.npc[(int)base.Projectile.ai[1]];
		}
		set
		{
			base.Projectile.ai[1] = value.whoAmI;
		}
	}

	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 8);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.timeLeft = 20;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return target == Target;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Target.Center;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		Texture2D chainTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/BrokenBiomeBlade_GrovetendersTouchChain", (AssetRequestMode)2).Value;
		float opacity = ((base.Projectile.timeLeft > 10) ? 1f : ((float)base.Projectile.timeLeft / 10f));
		Vector2 Shake = ((base.Projectile.timeLeft < 15) ? Vector2.Zero : (Vector2.One.RotatedByRandom(6.2831854820251465) * (15f - (float)base.Projectile.timeLeft / 5f) * 0.5f));
		Vector2 lineDirection = (Target.Center - NPCfrom.Center).SafeNormalize(Vector2.Zero);
		int dist = (int)Vector2.Distance(Target.Center, NPCfrom.Center) / 16;
		Vector2[] Nodes = (Vector2[])(object)new Vector2[dist + 1];
		Nodes[0] = NPCfrom.Center;
		Nodes[dist] = Target.Center;
		float pointUp = ((Target.Center.X > NPCfrom.Center.X) ? (-(float)Math.PI / 2f) : ((float)Math.PI / 2f));
		Vector2 scale = default(Vector2);
		Vector2 origin = default(Vector2);
		for (int i = 1; i < dist + 1; i++)
		{
			Vector2 positionAlongLine = Vector2.Lerp(NPCfrom.Center, Target.Center, (float)i / (float)dist);
			float elevation = (float)Math.Sin((float)i / (float)dist * (float)Math.PI) * 16f * (float)dist / 10f;
			Nodes[i] = positionAlongLine + lineDirection.RotatedBy(pointUp) * elevation + Shake * (float)Math.Sin((float)i / (float)dist * (float)Math.PI);
			float rotation = (Nodes[i] - Nodes[i - 1]).ToRotation() - (float)Math.PI / 2f;
			float yScale = Vector2.Distance(Nodes[i], Nodes[i - 1]) / (float)chainTex.Height;
			((Vector2)(ref scale))._002Ector(1f, yScale);
			Color chainLightColor = Lighting.GetColor((int)Nodes[i].X / 16, (int)Nodes[i].Y / 16);
			((Vector2)(ref origin))._002Ector((float)(chainTex.Width / 2), (float)chainTex.Height);
			Main.EntitySpriteDraw(chainTex, Nodes[i] - Main.screenPosition, null, chainLightColor * opacity, rotation, origin, scale, (SpriteEffects)0);
		}
		return false;
	}
}
