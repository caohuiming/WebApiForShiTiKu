/*
Navicat MySQL Data Transfer

Source Server         : MySQL57
Source Server Version : 50717
Source Host           : 127.0.0.1:3306
Source Database       : shitiku2

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2017-09-17 22:06:08
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for my_user
-- ----------------------------
DROP TABLE IF EXISTS `my_user`;
CREATE TABLE `my_user` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(50) NOT NULL,
  `user_phone` varchar(50) NOT NULL,
  `user_pwd` varchar(50) NOT NULL,
  `c_t` datetime NOT NULL,
  `u_t` datetime NOT NULL,
  `is_deleted` int(11) NOT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for shi_juan
-- ----------------------------
DROP TABLE IF EXISTS `shi_juan`;
CREATE TABLE `shi_juan` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '试卷编号',
  `shi_juan_name` varchar(200) NOT NULL COMMENT '试卷名称',
  `is_deleted` int(1) NOT NULL DEFAULT '0' COMMENT '是否已删除1：已删除，0：未删除',
  `c_t` datetime DEFAULT CURRENT_TIMESTAMP,
  `u_t` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for shi_ti
-- ----------------------------
DROP TABLE IF EXISTS `shi_ti`;
CREATE TABLE `shi_ti` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `shi_juan_id` int(11) DEFAULT NULL COMMENT '试卷编号',
  `shi_yong_deng_ji` int(11) DEFAULT NULL COMMENT '适用等级',
  `nan_du` varchar(50) DEFAULT NULL COMMENT '难度',
  `fen_shu` int(11) DEFAULT NULL COMMENT '答题分数',
  `shi_jian` int(11) DEFAULT NULL COMMENT '答题时间',
  `bian_hao` varchar(50) DEFAULT NULL COMMENT '试题编号',
  `ti_xing` varchar(20) DEFAULT NULL COMMENT '题型',
  `zheng_wen` text COMMENT '试题正文',
  `xuan_xiang` varchar(1000) DEFAULT NULL COMMENT '试题选项',
  `da_an` varchar(1000) DEFAULT NULL COMMENT '试题答案',
  `zbcsxx` varchar(1000) DEFAULT NULL COMMENT '自变参数选项',
  `csmsjj` varchar(1000) DEFAULT NULL COMMENT '参数M数据集',
  `ybcsnr` varchar(1000) DEFAULT NULL COMMENT '应变参数内容',
  `da_an_jie_xi` varchar(1000) DEFAULT NULL COMMENT '答案解析',
  `ping_fen_biao_zhun` varchar(1000) DEFAULT NULL COMMENT '答案要点及评分标准',
  `chu_chu` varchar(1000) DEFAULT NULL COMMENT '依据 出处',
  `chu_ti_ren` varchar(50) DEFAULT NULL COMMENT '出题人',
  `bei_zhu` varchar(1000) DEFAULT NULL COMMENT '备注',
  `tiao_mu` varchar(1000) DEFAULT NULL COMMENT '条目',
  `fen_ce_ming_cheng` varchar(50) DEFAULT NULL COMMENT '分册名称',
  `tiao_kuan` varchar(50) DEFAULT NULL COMMENT '条款',
  `zhuan_ye` varchar(50) DEFAULT NULL COMMENT '专业',
  `c_t` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `u_t` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '修改时间',
  `is_deleted` int(1) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=168 DEFAULT CHARSET=utf8;
