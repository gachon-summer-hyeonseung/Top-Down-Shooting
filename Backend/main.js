import Koa from "koa";
import Router from "@koa/router";
import { bodyParser } from "@koa/bodyparser";
import { MongoClient } from 'mongodb';
import { config } from "dotenv";

config();

const app = new Koa();
app.use(bodyParser());

const router = new Router();

const client = new MongoClient(`mongodb+srv://${process.env.DB_USER}:${process.env.DB_PASSWORD}@${process.env.DB_HOST}/?retryWrites=true&w=majority&appName=cocoa`);
const db = client.db("shooting");

router.get('/rank', async (ctx) => {
  const { page, limit } = ctx.request.query;
  const result = await db.collection("rank").find().sort({ score: -1, stage: 1, playTime: 1 }).skip((parseInt(page) - 1) * parseInt(limit)).limit(parseInt(limit)).toArray();

  ctx.body = {
    items: result,
  };
})

router.post('/rank', async (ctx) => {
  const { name, score, stage, playTime } = ctx.request.body;

  await db.collection("rank").insertOne({ name, score, stage, playTime });

  ctx.body = { success: true };
})

app.use(router.routes())
app.use(router.allowedMethods())

app.listen(3000, () => {
  console.log("Server is running on http://localhost:3000");
})