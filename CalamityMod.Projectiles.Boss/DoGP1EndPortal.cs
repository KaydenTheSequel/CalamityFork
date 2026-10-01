using System.Collections.Generic;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DoGP1EndPortal : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public ref float TimeCountdown => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 120);
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 60000;
		base.Projectile.hide = true;
	}

	public override void AI()
	{
		if (CalamityGlobalNPC.DoGHead < 0 || !Main.npc[CalamityGlobalNPC.DoGHead].active)
		{
			base.Projectile.active = false;
			base.Projectile.netUpdate = true;
			return;
		}
		if (TimeCountdown > 0f)
		{
			if (TimeCountdown > 120f)
			{
				base.Projectile.scale = MathHelper.Clamp(base.Projectile.scale + 0.02f, 0f, 1f);
			}
			if (TimeCountdown < 55f)
			{
				base.Projectile.scale = MathHelper.Clamp(base.Projectile.scale - 0.02f, 0f, 1f);
			}
			TimeCountdown--;
		}
		else
		{
			base.Projectile.scale = Utils.GetLerpValue(60000f, 59945f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(60f, 115f, Main.npc[CalamityGlobalNPC.DoGHead].localAI[2], clamped: true);
		}
		if ((Main.npc[CalamityGlobalNPC.DoGHead].localAI[2] < 60f && TimeCountdown == 0f) || CalamityGlobalNPC.DoGHead == -1 || TimeCountdown == 1f)
		{
			base.Projectile.Kill();
		}
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overWiresUI.Add(index);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion();
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/VoronoiShapes", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 origin = value.Size() * 0.5f;
		GameShaders.Misc["CalamityMod:DoGPortal"].UseOpacity(base.Projectile.scale);
		GameShaders.Misc["CalamityMod:DoGPortal"].UseColor(Color.Cyan);
		GameShaders.Misc["CalamityMod:DoGPortal"].UseSecondaryColor(Color.Fuchsia);
		GameShaders.Misc["CalamityMod:DoGPortal"].Apply();
		Main.EntitySpriteDraw(value, drawPosition, null, Color.White, 0f, origin, 3.5f, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}
}
