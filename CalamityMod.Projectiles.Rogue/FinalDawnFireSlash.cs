using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Rogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class FinalDawnFireSlash : ModProjectile, ILocalizedModType, IModType
{
	public bool HasRegeneratedStealth;

	public static float StealthReturnRatio = 0.4f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 11;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 300;
		base.Projectile.height = 398;
		base.Projectile.friendly = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (player == null || player.dead)
		{
			base.Projectile.Kill();
		}
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		AdjustPlayerPositionValues(player);
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 4f)
		{
			base.Projectile.ai[1]++;
			base.Projectile.ai[0] = 0f;
			if (base.Projectile.ai[1] == 5f)
			{
				base.Projectile.friendly = true;
				SoundEngine.PlaySound(in TheFinalDawn.UseSound, base.Projectile.Center);
			}
		}
		if (base.Projectile.ai[1] >= 11f)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.frame = (int)base.Projectile.ai[1];
		AdjustPlayerItemFrameValues(player);
	}

	public void AdjustPlayerPositionValues(Player player)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = player.Center;
		base.Projectile.position.X += 60 * player.direction;
		base.Projectile.position.Y -= 30f;
	}

	public void AdjustPlayerItemFrameValues(Player player)
	{
		if (base.Projectile.ai[1] < 5f)
		{
			player.bodyFrame.Y = player.bodyFrame.Height;
		}
		else if (base.Projectile.ai[1] < 8f)
		{
			player.bodyFrame.Y = 3 * player.bodyFrame.Height;
		}
		else
		{
			player.bodyFrame.Y = 4 * player.bodyFrame.Height;
		}
		base.Projectile.spriteDirection = player.direction;
		player.heldProj = base.Projectile.whoAmI;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		CalamityPlayer calamityPlayer = Main.player[base.Projectile.owner].Calamity();
		if (!HasRegeneratedStealth && !base.Projectile.Calamity().LocketClone)
		{
			calamityPlayer.rogueStealth += calamityPlayer.rogueStealthMax * StealthReturnRatio;
			if (calamityPlayer.rogueStealth > calamityPlayer.rogueStealthMax)
			{
				calamityPlayer.rogueStealth = calamityPlayer.rogueStealthMax;
			}
			HasRegeneratedStealth = true;
		}
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		int width = 306;
		int height = 398;
		Vector2 drawCenter = base.Projectile.Center;
		Rectangle frameRectangle = default(Rectangle);
		((Rectangle)(ref frameRectangle))._002Ector(base.Projectile.frame / 5 * width, base.Projectile.frame % 5 * height, width, height);
		Texture2D scytheTexture = TextureAssets.Projectile[base.Type].Value;
		Texture2D glowTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/FinalDawnFireSlash_Glow", (AssetRequestMode)2).Value;
		Main.spriteBatch.Draw(scytheTexture, drawCenter - Main.screenPosition, (Rectangle?)frameRectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, frameRectangle.Size() / 2f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1), 0f);
		Main.spriteBatch.Draw(glowTexture, drawCenter - Main.screenPosition, (Rectangle?)frameRectangle, base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, frameRectangle.Size() / 2f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1), 0f);
		return false;
	}
}
