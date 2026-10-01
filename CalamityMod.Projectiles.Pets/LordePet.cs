using CalamityMod.CalPlayer;
using CalamityMod.NPCs.Other;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class LordePet : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Pets";

	public override string Texture => "CalamityMod/NPCs/Other/THELORDE";

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = (base.Projectile.height = 67);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.aiStyle = 26;
		base.AIType = 197;
		base.Projectile.scale = 0.3f;
		base.DrawOriginOffsetX -= 150f;
		base.DrawOriginOffsetY -= 150;
	}

	public override void AI()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		if (player.dead)
		{
			modPlayer.lordePet = false;
		}
		if (modPlayer.lordePet)
		{
			base.Projectile.timeLeft = 2;
		}
		Vector2 center = base.Projectile.Center;
		Color discoColor = Main.DiscoColor;
		Lighting.AddLight(center, ((Color)(ref discoColor)).ToVector3() * 2f);
		if (Main.rand.NextBool(1200))
		{
			SoundStyle style = THELORDE.DeathSound with
			{
				PitchVariance = 2f,
				MaxInstances = 5
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
	}

	public override bool PreDraw(ref Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frameUsed = texture.Frame(2, 7, 0, 1);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), frameUsed, drawColor, base.Projectile.rotation, new Vector2((float)texture.Width / 4f, (float)texture.Height / 14f), base.Projectile.scale, spriteEffects);
		return false;
	}
}
