-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Client :  127.0.0.1
-- Généré le :  Ven 25 Novembre 2016 à 15:55
-- Version du serveur :  10.1.19-MariaDB
-- Version de PHP :  5.6.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `stpierre`
--
CREATE DATABASE IF NOT EXISTS `stpierre` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `stpierre`;

-- --------------------------------------------------------

--
-- Structure de la table `brand`
--

DROP TABLE IF EXISTS `brand`;
CREATE TABLE IF NOT EXISTS `brand` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `phone` varchar(30) DEFAULT NULL,
  `contact` varchar(255) DEFAULT NULL,
  `website` varchar(127) DEFAULT NULL,
  `note` text,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4;

--
-- Structure de la table `category`
--

DROP TABLE IF EXISTS `category`;
CREATE TABLE IF NOT EXISTS `category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `description` text,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `category`
--

INSERT INTO `category` (`id`, `name`, `description`) VALUES
(2, 'Articles', 'Accessoires pour les équipements'),
(1, 'Équipement', 'Items ayant un moteur'),
(3, 'Autre', 'Items divers, sans lien avec les équipements ou articles');

-- --------------------------------------------------------

--
-- Structure de la table `company`
--

DROP TABLE IF EXISTS `company`;
CREATE TABLE IF NOT EXISTS `company` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(127) NOT NULL,
  `description` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `company`
--

INSERT INTO `company` (`id`, `name`, `description`) VALUES
(3, 'Transport St-Pierre', ''),
(2, 'Excavation St-Pierre', ''),
(4, 'Démolition St-Pierre', ''),
(1, 'Inconnue', 'Le la compagnie pour la qu''elle l''objet est attitré est inconnue ou pour plusieurs de ceux-ci');

-- --------------------------------------------------------

--
-- Structure de la table `item`
--

DROP TABLE IF EXISTS `item`;
CREATE TABLE IF NOT EXISTS `item` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `description` text NOT NULL,
  `number` varchar(10) DEFAULT NULL,
  `model` varchar(127) DEFAULT NULL,
  `year` smallint(6) DEFAULT NULL,
  `value` decimal(10,0) DEFAULT NULL,
  `comments` text,
  `receptionDate` datetime DEFAULT NULL,
  `creationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `serialNumber` varchar(255) DEFAULT NULL,
  `matriculation` varchar(10) DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL,
  `FK_Type_id` int(11) NOT NULL,
  `FK_Brand_id` int(11) DEFAULT NULL,
  `FK_Location_id` int(11) DEFAULT NULL,
  `FK_Provider_id` int(11) DEFAULT NULL,
  `FK_Company_id` int(11) DEFAULT NULL,
  `FK_Unit_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4;

--
-- Structure de la table `item_compatibility`
--

DROP TABLE IF EXISTS `item_compatibility`;
CREATE TABLE IF NOT EXISTS `item_compatibility` (
  `FK_Item1_id` int(11) NOT NULL,
  `FK_Item2_id` int(11) NOT NULL,
  PRIMARY KEY (`FK_Item1_id`,`FK_Item2_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4;

--
-- Structure de la table `location`
--

DROP TABLE IF EXISTS `location`;
CREATE TABLE IF NOT EXISTS `location` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(127) NOT NULL,
  `description` text,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `provider`
--

DROP TABLE IF EXISTS `provider`;
CREATE TABLE IF NOT EXISTS `provider` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `phone` varchar(30) DEFAULT NULL,
  `phoneAlt` varchar(30) DEFAULT NULL,
  `email` varchar(127) DEFAULT NULL,
  `contact` varchar(255) DEFAULT NULL,
  `website` varchar(127) DEFAULT NULL,
  `city` varchar(127) DEFAULT NULL,
  `notes` text,
  `creationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `role`
--

DROP TABLE IF EXISTS `role`;
CREATE TABLE IF NOT EXISTS `role` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `description` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `role`
--

INSERT INTO `role` (`id`, `name`, `description`) VALUES
(1, 'Administration', 'L''administration peut voir tout les champs ainsi que de créer et modifier les autres utilisateurs'),
(2, 'Mécanicien', 'Le mécanicien est l''utilisateur général.\r\nIl ne peut modifier les informations, Il peut seulement lire certains champs. ');

-- --------------------------------------------------------

--
-- Structure de la table `type`
--

DROP TABLE IF EXISTS `type`;
CREATE TABLE IF NOT EXISTS `type` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `FK_Category_id` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `unit`
--

DROP TABLE IF EXISTS `unit`;
CREATE TABLE IF NOT EXISTS `unit` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `shortName` varchar(10) NOT NULL,
  `description` text,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `unit`
--

INSERT INTO `unit` (`id`, `name`, `shortName`, `description`) VALUES
(2, 'Litres', 'L', 'Volume en Litres'),
(1, 'Kilogramme', 'kg', 'Poids en Kg'),
(3, 'onces', 'oz', 'Quantité liquide onces'),
(4, 'livre', 'lbs', 'Poids en livre');

-- --------------------------------------------------------

--
-- Structure de la table `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `firstName` varchar(127) NOT NULL,
  `lastName` varchar(127) NOT NULL,
  `email` varchar(127) NOT NULL,
  `phone` varchar(30) NOT NULL,
  `phoneAlt` varchar(30) DEFAULT NULL,
  `userCode` char(6) NOT NULL,
  `passwordHash` varchar(255) NOT NULL,
  `note` text,
  `creationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `archiveDate` datetime DEFAULT NULL,
  `isActive` tinyint(1) NOT NULL DEFAULT '1',
  `FK_Role_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `user`
--

INSERT INTO `user` (`id`, `username`, `firstName`, `lastName`, `email`, `phone`, `phoneAlt`, `userCode`, `passwordHash`, `note`, `creationDate`, `archiveDate`, `isActive`, `FK_Role_id`) VALUES
(1, 'Admin', 'admin', 'admin', 'admin@gmail.com', '911', '911', 'admin', 'dd94709528bb1c83d08f3088d4043f4742891f4f', 'Administrateur du système', '2016-11-11 10:11:44', NULL, 1, 1);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
