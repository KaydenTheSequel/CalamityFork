using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Other;

public class DemonPortal : ModNPC
{
	public ref float Time => ref base.NPC.ai[0];

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
	}

	public override void SetDefaults()
	{
		base.NPC.width = (base.NPC.height = 60);
		base.NPC.damage = 0;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 25000;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.knockBackResist = 0f;
		base.NPC.netAlways = true;
		base.NPC.aiStyle = -1;
		base.NPC.Calamity().ProvidesProximityRage = false;
		base.NPC.Calamity().DoesNotDisappearInBossRush = true;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = 25000;
	}

	public override void AI()
	{
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life == 1 && base.NPC.ai[1] <= 0f)
		{
			base.NPC.ai[1] = 1f;
			base.NPC.dontTakeDamage = true;
			base.NPC.netUpdate = true;
			int demonType = ModContent.ProjectileType<SuicideBomberDemon>();
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == demonType && !p.hostile)
				{
					p.hostile = true;
					p.friendly = false;
					p.netUpdate = true;
				}
			}
		}
		base.NPC.rotation += 0.18f;
		base.NPC.Opacity = Utils.GetLerpValue(0f, 30f, Time, clamped: true) * Utils.GetLerpValue(420f, 390f, Time, clamped: true);
		base.NPC.velocity = Vector2.Zero;
		base.NPC.scale = base.NPC.Opacity;
		if (Time == 300f)
		{
			if (Main.myPlayer == base.NPC.target)
			{
				ReleaseThings();
			}
			SoundEngine.PlaySound(in SoundID.DD2_EtherianPortalOpen, base.NPC.Center);
		}
		if (Main.netMode != 1 && Time >= 420f)
		{
			base.NPC.active = false;
			base.NPC.netUpdate = true;
		}
		Time++;
	}

	public void ReleaseThings()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		bool friendly = base.NPC.life == 1;
		for (int i = 0; i < 6; i++)
		{
			int demon = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Main.rand.NextVector2CircularEdge(4f, 4f), ModContent.ProjectileType<SuicideBomberDemon>(), 17000, 0f, base.NPC.target);
			if (Main.projectile.IndexInRange(demon))
			{
				Main.projectile[demon].ai[1] = Main.rand.Next(-40, 0);
				Main.projectile[demon].friendly = friendly;
				Main.projectile[demon].hostile = !friendly;
				Main.projectile[demon].netUpdate = true;
			}
		}
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.type == ModContent.ProjectileType<SuicideBomberDemon>())
		{
			return false;
		}
		return null;
	}

	public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
	{
		modifiers.SetMaxDamage(base.NPC.life - 1);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		spriteBatch.SetBlendState(BlendState.AlphaBlend);
		Texture2D portalTexture = TextureAssets.Npc[base.Type].Value;
		Vector2 drawPosition = base.NPC.Center - screenPos;
		Vector2 origin = portalTexture.Size() * 0.5f;
		Color white = Color.White;
		Color color = Color.Lerp(white, Color.Black, 0.55f) * base.NPC.Opacity * 1.8f;
		spriteBatch.Draw(portalTexture, drawPosition, (Rectangle?)null, color, base.NPC.rotation, origin, base.NPC.scale * 1.2f, (SpriteEffects)0, 0f);
		spriteBatch.Draw(portalTexture, drawPosition, (Rectangle?)null, color, 0f - base.NPC.rotation, origin, base.NPC.scale * 1.2f, (SpriteEffects)0, 0f);
		color = Color.Lerp(Color.Lerp(white, Color.Purple, 0.55f), Color.Black, 0.66f) * base.NPC.Opacity * 1.6f;
		spriteBatch.Draw(portalTexture, drawPosition, (Rectangle?)null, color, base.NPC.rotation * 0.6f, origin, base.NPC.scale * 1.2f, (SpriteEffects)0, 0f);
		color = Color.Lerp(white, Color.Red, 0.55f) * base.NPC.Opacity * 1.6f;
		spriteBatch.Draw(portalTexture, drawPosition, (Rectangle?)null, color, base.NPC.rotation * -0.6f, origin, base.NPC.scale * 1.2f, (SpriteEffects)0, 0f);
		spriteBatch.SetBlendState(BlendState.AlphaBlend);
		return false;
	}
}
