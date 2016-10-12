# nxtリモコンアプリケーション

### 動作条件
.Net Framework 3.5 以上

### 動作検証環境
Windows 7 Professional

### 開発環境
Visual Studio C# 2010以降

### Bluetooth環境セットアップ
1. AbeのBluetoothドングルをPCにインストールする
2. ロボット側でBluetoothの設定画面に移動し、Searchを行い、ドングルをインストールいたPCと接続する。
3. 接続を行おうとすると、PC側でpass keyによる認証が行われるので、「1234」を入力する


### コンパイル手順
1. NxtLogger.slnを開き、任意の構成（[Debug] or [Release]）でビルドする  
（ビルドすると自動的にStyleCopによるチェックが始まるので、警告が指摘されていないことを確認する）
2. NxtLoggerをスタートアッププロジェクトに設定する


### 動作手順
1. PCとNXTのBluetooth接続を確立する（事前準備）。
2. ロボットを起動し、RUN→「BD_ADDR」が表示される画面まで進める。
3. NxtLoggerを実行し、表示されたフォームにてBluetooth接続したポートを選択する。
4. NxtLoggerのフォームにてポートが選択出来たら、CONNECTをクリックする。
5. 画面右上の「CTS」と「DSR」がアクティブになるのを待つ(グレーアウト→黒字に変わる)。  
接続に成功するとロボットの画面には[R][BT]が表示される。
6. ロボットは「RUN」を選択し、Gyro調整を行い、タッチセンサを押下し尻尾をおろすところまで進める。
7. NxtLoggerで「Mission」ボタンを押下する。Missionウィンドウが開く。
8. Missionウィンドウで、[w][a][s][z]キーと速度スライドバーを使ってロボットに命令を送る。  
 [w] 前進 [a] 左回転 [s] 右回転 [z] 後退
