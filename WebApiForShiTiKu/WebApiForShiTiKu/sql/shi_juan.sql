/*
Navicat MySQL Data Transfer

Source Server         : MySQL57
Source Server Version : 50717
Source Host           : 127.0.0.1:3306
Source Database       : shitiku2

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2017-09-17 18:56:53
*/

SET FOREIGN_KEY_CHECKS=0;

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
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of shi_juan
-- ----------------------------
INSERT INTO `shi_juan` VALUES ('1', 'shijuan1', '0', '2017-09-16 10:54:39', '2017-09-17 17:49:10');
INSERT INTO `shi_juan` VALUES ('2', 'shijuan21', '0', '2017-09-16 11:17:34', '2017-09-17 17:49:16');
INSERT INTO `shi_juan` VALUES ('3', 'shijuan3', '0', '2017-09-16 20:20:52', '2017-09-17 17:35:33');
INSERT INTO `shi_juan` VALUES ('4', '题库导入模板4', '0', '2017-09-17 17:16:18', '2017-09-17 17:17:09');
INSERT INTO `shi_juan` VALUES ('5', '题库导入模板2017-09-17_175141', '0', '2017-09-17 17:51:41', '2017-09-17 17:51:41');
INSERT INTO `shi_juan` VALUES ('6', '题库导入模板2017-09-17_175300', '1', '2017-09-17 17:53:00', '2017-09-17 17:55:08');
