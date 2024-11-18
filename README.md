#  AvoidPedestrians

救急車を操作して時間内に病院を目指すゲーム.  
道中に人がわんさかいるので轢かないように.  
Unityのカスタマイズできるテンプレを元に作成した
（
<a href="https://learn.unity.com/project/karting-microgame?language=ja" target="_blank" rel="noopener noreferrer">3D ゴーカートレースの Microgame</a>
）. 

歩行者の挙動は引力と斥力で表現されている
（参考：
<a href="https://journals.aps.org/pre/abstract/10.1103/PhysRevE.51.4282" target="_blank" rel="noopener noreferrer">Social force model for pedestrian dynamics</a>
）.  
力の数式はなんとなくで記述しているので再現性はそこまで高くない.  
歩行者を多数配置し, 創発的な歩行者流が形成されるというマルチエージェントシステムを模しているつもり.

ファイル管理の都合上, 一度リポジトリを作り直して必要最小限？のファイルしかあげていないので, ファイルを取り込んでもうまく動作しないかも. あとWebブラウザ版では, プレイ中にポーズ画面を開くと何故かフリーズする.  
**というかそもそもWebブラウザ版だと処理が重くなって動作がカタつくかも.**