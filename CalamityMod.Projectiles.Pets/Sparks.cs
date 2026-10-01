using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class Sparks : ModProjectile, ILocalizedModType, IModType
{
	private int color;

	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.LightPet[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 44;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
	}

	private void Pickup()
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Rectangle val;
		for (int itemIndex = 0; itemIndex < Main.maxItems; itemIndex++)
		{
			Item item = Main.item[itemIndex];
			if (!item.active || item.noGrabDelay != 0 || item.playerIndexTheItemIsReservedFor != base.Projectile.owner || !ItemLoader.CanPickup(item, player))
			{
				continue;
			}
			val = new Rectangle((int)base.Projectile.position.X, (int)base.Projectile.position.Y, base.Projectile.width, base.Projectile.height);
			if (!((Rectangle)(ref val)).Intersects(new Rectangle((int)item.position.X, (int)item.position.Y, item.width, item.height)) || base.Projectile.owner != Main.myPlayer || (player.HeldItem.type == 0 && player.itemAnimation > 0))
			{
				continue;
			}
			if (!ItemLoader.OnPickup(item, player))
			{
				Main.item[itemIndex] = new Item();
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, null, itemIndex);
				}
			}
			else if (!ItemID.Sets.NebulaPickup[item.type] && item.type != 58 && item.type != 1734 && item.type != 1867 && item.type != 184 && item.type != 1735 && item.type != 1868)
			{
				Main.item[itemIndex] = player.GetItem(base.Projectile.owner, item, default(GetItemSettings));
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, null, itemIndex);
				}
			}
		}
		for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
		{
			NPC npc = Main.npc[npcIndex];
			if (npc.active && (npc.type == 356 || npc.type == 444 || npc.type == 653))
			{
				val = new Rectangle((int)base.Projectile.position.X, (int)base.Projectile.position.Y, base.Projectile.width, base.Projectile.height);
				if (((Rectangle)(ref val)).Intersects(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height)))
				{
					npc.life = 0;
					npc.active = false;
					SoundEngine.PlaySound(in SoundID.Item2, base.Projectile.position);
					npc.netUpdate = true;
				}
			}
		}
	}

	private void PassiveAI()
	{
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		float SAImovement = 0.05f;
		for (int index = 0; index < Main.projectile.Length; index++)
		{
			Projectile proj = Main.projectile[index];
			bool isPet = Main.projPet[proj.type];
			if (((index != base.Projectile.whoAmI && proj.active && proj.owner == base.Projectile.owner) & isPet) && Math.Abs(base.Projectile.position.X - proj.position.X) + Math.Abs(base.Projectile.position.Y - proj.position.Y) < (float)base.Projectile.width)
			{
				if (base.Projectile.position.X < proj.position.X)
				{
					base.Projectile.velocity.X -= SAImovement;
				}
				else
				{
					base.Projectile.velocity.X += SAImovement;
				}
				if (base.Projectile.position.Y < proj.position.Y)
				{
					base.Projectile.velocity.Y -= SAImovement;
				}
				else
				{
					base.Projectile.velocity.Y += SAImovement;
				}
			}
		}
		float flySpeed = 0.5f;
		base.Projectile.tileCollide = false;
		Vector2 flyDirection = base.Projectile.Center;
		float xDist = player.position.X + (float)(player.width / 2) - flyDirection.X;
		float yDist = player.position.Y + (float)(player.height / 2) - flyDirection.Y;
		yDist += (float)Main.rand.Next(-10, 21);
		xDist += (float)Main.rand.Next(-10, 21);
		xDist += 60f * (float)player.direction;
		yDist -= 60f;
		float playerDist = (float)Math.Sqrt(xDist * xDist + yDist * yDist);
		if (playerDist < 160f)
		{
			base.Projectile.ai[0] = 0f;
		}
		if (playerDist < 100f && player.velocity.Y == 0f && base.Projectile.position.Y + (float)base.Projectile.height <= player.position.Y + (float)player.height && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height) && base.Projectile.velocity.Y < -6f)
		{
			base.Projectile.velocity.Y = -6f;
		}
		if (playerDist > 2000f)
		{
			base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.height / 2);
			base.Projectile.netUpdate = true;
		}
		if (playerDist < 50f)
		{
			if (Math.Abs(base.Projectile.velocity.X) > 2f || Math.Abs(base.Projectile.velocity.Y) > 2f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.99f;
			}
			flySpeed = 0.01f;
		}
		else
		{
			if (playerDist < 100f)
			{
				flySpeed = 0.1f;
			}
			if (playerDist > 300f)
			{
				flySpeed = 1f;
			}
			playerDist = 18f / playerDist;
			xDist *= playerDist;
			yDist *= playerDist;
		}
		if (base.Projectile.velocity.X < xDist)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X + flySpeed;
			if (flySpeed > 0.05f && base.Projectile.velocity.X < 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X + flySpeed;
			}
		}
		if (base.Projectile.velocity.X > xDist)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X - flySpeed;
			if (flySpeed > 0.05f && base.Projectile.velocity.X > 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X - flySpeed;
			}
		}
		if (base.Projectile.velocity.Y < yDist)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + flySpeed;
			if (flySpeed > 0.05f && base.Projectile.velocity.Y < 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y + flySpeed * 2f;
			}
		}
		if (base.Projectile.velocity.Y > yDist)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y - flySpeed;
			if (flySpeed > 0.05f && base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - flySpeed * 2f;
			}
		}
	}

	public override void AI()
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (player.statLife >= (int)((double)player.statLifeMax2 * 0.75))
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				color = 0;
			}
		}
		else if (player.statLife >= (int)((double)player.statLifeMax2 * 0.5))
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				color = 1;
			}
		}
		else if (Main.myPlayer == base.Projectile.owner)
		{
			color = 2;
		}
		if (color == 0)
		{
			Lighting.AddLight(base.Projectile.Center, 1.5f, 1.5f, 0.1f);
		}
		else if (color == 1)
		{
			Lighting.AddLight(base.Projectile.Center, 0.1f, 0.1f, 1.5f);
		}
		else
		{
			Lighting.AddLight(base.Projectile.Center, 0.1f, 1.5f, 0.1f);
		}
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 4)
		{
			base.Projectile.frame = 0;
		}
		if ((double)base.Projectile.velocity.X >= 0.25)
		{
			base.Projectile.direction = 1;
		}
		else if ((double)base.Projectile.velocity.X < -0.25)
		{
			base.Projectile.direction = -1;
		}
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		if (player.dead)
		{
			modPlayer.sparks = false;
		}
		if (modPlayer.sparks)
		{
			base.Projectile.timeLeft = 2;
		}
		Pickup();
		bool decelerate = false;
		if (base.Projectile.ai[0] == 2f)
		{
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] > 30f)
			{
				base.Projectile.ai[1] = 1f;
				base.Projectile.ai[0] = 0f;
				base.Projectile.numUpdates = 0;
				base.Projectile.netUpdate = true;
			}
			else
			{
				decelerate = true;
			}
		}
		if (decelerate)
		{
			return;
		}
		Vector2 targetLocation = base.Projectile.position;
		bool foundFood = false;
		float range = 160f;
		for (int itemIndex = 0; itemIndex < Main.maxItems; itemIndex++)
		{
			Item item = Main.item[itemIndex];
			if (item.active && item.noGrabDelay == 0 && item.playerIndexTheItemIsReservedFor == base.Projectile.owner && ItemLoader.CanPickup(item, Main.player[item.playerIndexTheItemIsReservedFor]) && Main.player[item.playerIndexTheItemIsReservedFor].ItemSpace(item).CanTakeItemToPersonalInventory && !ItemID.Sets.NebulaPickup[item.type] && item.type != 58 && item.type != 1734 && item.type != 1867 && item.type != 184 && item.type != 1735 && item.type != 1868)
			{
				float itemDist = Vector2.Distance(item.Center, base.Projectile.Center);
				if (Vector2.Distance(base.Projectile.Center, targetLocation) < itemDist && itemDist < range)
				{
					range = itemDist;
					targetLocation = item.Center;
					foundFood = true;
				}
			}
		}
		for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
		{
			NPC npc = Main.npc[npcIndex];
			if (npc.active && (npc.type == 356 || npc.type == 444))
			{
				float npcDist = Vector2.Distance(npc.Center, base.Projectile.Center);
				if (Vector2.Distance(base.Projectile.Center, targetLocation) < npcDist && npcDist < range)
				{
					range = npcDist;
					targetLocation = npc.Center;
					foundFood = true;
				}
			}
		}
		float separationAnxietyDist = 100f;
		if (foundFood)
		{
			separationAnxietyDist = 200f;
		}
		if (Vector2.Distance(player.Center, base.Projectile.Center) > separationAnxietyDist)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.netUpdate = true;
		}
		if (foundFood && base.Projectile.ai[0] == 0f)
		{
			Vector2 targetDirection = targetLocation - base.Projectile.Center;
			float num = ((Vector2)(ref targetDirection)).Length();
			((Vector2)(ref targetDirection)).Normalize();
			if (num > 200f)
			{
				float scaleFactor2 = 18f;
				targetDirection *= scaleFactor2;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + targetDirection) / 41f;
			}
			else
			{
				targetDirection *= -9f;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + targetDirection) / 41f;
			}
		}
		else
		{
			PassiveAI();
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1] += Main.rand.Next(1, 3);
		}
		if (base.Projectile.ai[1] > 30f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[0] == 0f && ((base.Projectile.ai[1] == 0f) & foundFood) && range < 300f)
		{
			base.Projectile.ai[1]++;
			if (Main.myPlayer == base.Projectile.owner)
			{
				base.Projectile.ai[0] = 2f;
				Vector2 targetDest = targetLocation - base.Projectile.Center;
				((Vector2)(ref targetDest)).Normalize();
				base.Projectile.velocity = targetDest * 12f;
				base.Projectile.netUpdate = true;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		if (color == 1)
		{
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/SparksBlue", (AssetRequestMode)2).Value;
		}
		if (color == 2)
		{
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/SparksGreen", (AssetRequestMode)2).Value;
		}
		int height = texture.Height / Main.projFrames[base.Type];
		int frameHeight = height * base.Projectile.frame;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, frameHeight, texture.Width, height), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)height / 2f), base.Projectile.scale, spriteEffects, 0f);
		return false;
	}
}
