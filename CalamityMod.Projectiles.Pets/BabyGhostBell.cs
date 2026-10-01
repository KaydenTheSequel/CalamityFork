using CalamityMod.CalPlayer;
using CalamityMod.Graphics;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class BabyGhostBell : ModProjectile, ILocalizedModType, IModType
{
	private bool underwater;

	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.LightPet[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
	}

	public override void AI()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (player.dead)
		{
			modPlayer.babyGhostBell = false;
		}
		if (modPlayer.babyGhostBell)
		{
			base.Projectile.timeLeft = 2;
		}
		underwater = Collision.DrownCollision(player.position, player.width, player.height, player.gravDir);
		if (underwater)
		{
			if (Main.LocalPlayer.Calamity().ZoneAbyss)
			{
				EnhancedDarknessSystem.lights.Add(new EnhancedDarknessSystem.LightSource
				{
					center = base.Projectile.Center,
					rotation = 0f,
					scale = 3f,
					texture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value
				});
			}
			Lighting.AddLight(base.Projectile.Center, 0.3f, 0.9f, 1.5f);
		}
		else
		{
			Lighting.AddLight(base.Projectile.Center, 0.1f, 0.3f, 0.5f);
		}
		base.Projectile.FloatingPetAI(faceRight: false, 0.05f, lightPet: true);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
	}
}
